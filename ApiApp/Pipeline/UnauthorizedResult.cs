using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    public class UnauthorizedResult : ObjectResult
    {
        #region Class Variables

        private static int _statusCode = (int)HttpStatusCode.Unauthorized;

        #endregion

        #region Constructors

        public UnauthorizedResult(object error)
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
