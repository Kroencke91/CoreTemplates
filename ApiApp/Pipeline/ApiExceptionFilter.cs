﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
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
        public void OnException(ExceptionContext context)
        {
            var response = context.HttpContext.Response;

            var exceptionType = context.Exception.GetType();

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
    }
}