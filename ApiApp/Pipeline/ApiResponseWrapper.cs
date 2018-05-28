using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
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

            var currentBody = response.Body;

            using (var memoryStream = new MemoryStream())
            {
                response.Body = memoryStream;

                await _next(context);

                var apiVersionProperties = context.ApiVersionProperties();

                var apiVersion = apiVersionProperties.RawApiVersion;

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

                string statusMessage;

                var sb = new StringBuilder();

                sb.Append(Enum.GetName(typeof(HttpStatusCode), statusCode));

                if (statusCode > 399)
                {
                    if (statusCode == 401)
                    {
                        var authErrors = response.Headers["WWW-Authenticate"];

                        foreach (var authErr in authErrors)
                        {
                            sb.Append($" - {authErr.Replace("\"", "'")}");
                        }
                    }
                }

                statusMessage = sb.ToString();

                var result = new ApiResponse((HttpStatusCode)statusCode, statusMessage, reqestUrl, apiVersion, objResult);

                response.ContentType = response.ContentType ?? "application/json; charset=utf-8";

                await response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
