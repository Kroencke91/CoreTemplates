using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    public class ExceptionResult : ObjectResult
    {
        #region Class Variables

        private static int _statusCode = (int)HttpStatusCode.InternalServerError;

        #endregion

        #region Constructors

        public ExceptionResult(object error)
            : base(error)
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
