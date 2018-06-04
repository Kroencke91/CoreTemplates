using ApiApp.Interfaces;
using ApiApp.Misc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        #region Class Variables

        private static IAppInfo _appInfo = Startup.AppInfo;

        private static ILogger _log = _appInfo.Env.Log;

        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public Methods

        public void OnException(ExceptionContext context)
        {
            var response = context.HttpContext.Response;

            var exceptionType = context.Exception?.GetType() ?? typeof(NullException);

            try
            {
                //TEST: throw new ApplicationException("Test ApiExceptionFilter Exception Handling");

                //REFACTOR: Use ConcurrentDictionary instead
                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else if (exceptionType == typeof(ValidationException))
                {
                    response.StatusCode = 422; //Unprocessable Entity
                }
                else if (exceptionType == typeof(NotImplementedException))
                {
                    response.StatusCode = (int)HttpStatusCode.NotImplemented;
                }
                else if (exceptionType == typeof(TimeoutException))
                {
                    response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                var apiError = new ApiError(context.Exception);

                context.ExceptionHandled = true;

                response.WriteAsync(JsonConvert.SerializeObject(apiError));
            }
            catch (Exception ex)
            {
                _log.Error($"{ex}");

                var apiError = new ApiError(ex);

                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                response.ContentType = response.ContentType ?? "application/json; charset=utf-8";

                context.ExceptionHandled = true;

                response.WriteAsync(JsonConvert.SerializeObject(apiError));
            }
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
