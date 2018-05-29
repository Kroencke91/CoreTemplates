using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    public class ValidationFailedResult : ObjectResult
    {
        #region Class Variables

        private static int _statusCode = StatusCodes.Status422UnprocessableEntity;

        #endregion

        #region Constructors

        public ValidationFailedResult(string code, string message, ModelStateDictionary modelState)
        : base(new ApiError(code, message, modelState))
        {
            StatusCode = _statusCode;
        }
        
        #endregion

        #region Properties
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
