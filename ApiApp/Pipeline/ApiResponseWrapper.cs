using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    public class ApiResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ApiResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var reqestUrl = context.Request.GetDisplayUrl();

            var response = context.Response;

            try
            {
                var currentBody = response.Body;

                using (var memoryStream = new MemoryStream())
                {
                    response.Body = memoryStream;

                    await _next(context);

                    var apiVersion = context.ApiVersionProperties().RawApiVersion ?? "0.0";

                    //ALT: var apiVersion = context.GetRequestedApiVersion() == null ? "0.0" : context.GetRequestedApiVersion().ToString();

                    //TEST: throw new ApplicationException("Wrapper Test");
                    
                    response.Body = currentBody;

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var readToEnd = new StreamReader(memoryStream).ReadToEnd();

                    var statusCode = response.StatusCode;

                    object objResult = null;

                    try
                    {
                        objResult = statusCode < 400 ? JsonConvert.DeserializeObject(readToEnd) : JsonConvert.DeserializeObject<ApiError>(readToEnd);
                    }
                    catch (Exception ex)
                    {
                        //TODO: Log Unknown Error Response Content

                        objResult = JsonConvert.DeserializeObject(readToEnd);
                    }

                    string statusMessage = Enum.GetName(typeof(HttpStatusCode), statusCode); //TODO: Convert to Dictionary for performance?
                    
                    if (statusCode > 399)
                    {
                        switch (statusCode)
                        {
                            case 401: //Unauthorized

                                var authErrors = response.Headers["WWW-Authenticate"];

                                var errMsg = string.Empty;

                                foreach (var authErr in authErrors)
                                {
                                    errMsg = authErr.Replace("\"", "'");

                                    break;
                                }

                                objResult = new UnauthorizedError(errMsg);

                                break;

                            case 403: //Forbidden

                                objResult = new ForbiddenError("Access Denied");

                                break;

                            case 422: //UnprocessableEntity

                                statusMessage = "UnprocessableEntity";

                                break;

                            default:

                                //TODO: Log unhandled HttpStatusCode for future enhancement

                                break;
                        }
                    }

                    var result = new ApiResponse((HttpStatusCode)statusCode, statusMessage, reqestUrl, apiVersion, objResult);

                    response.ContentType = response.ContentType ?? "application/json; charset=utf-8";

                    await response.WriteAsync(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception ex)
            {
                //TODO: Add logging

                //TODO: Figure out how to handle!

                ////var exContext = new ExceptionContext(context, null);

                ////((ExceptionContext)context).ExceptionHandled = true;

                //var apiError = new ApiError(ex);

                //var objResult = new ExceptionResult(apiError);

                //var result = new ApiResponse(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), reqestUrl, null, objResult);

                //response.ContentType = response.ContentType ?? "application/json; charset=utf-8";

                //await response.WriteAsync(JsonConvert.SerializeObject(result));

                //var apiError = new ApiError(ex);

                //response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //response.ContentType = response.ContentType ?? "application/json; charset=utf-8";

                ////context.ExceptionHandled = true;

                //await response.WriteAsync(JsonConvert.SerializeObject(apiError));

                //response.StatusCode = (int)HttpStatusCode.OK;

                //await response.WriteAsync("Hello World");
            }
        }
    }
}
