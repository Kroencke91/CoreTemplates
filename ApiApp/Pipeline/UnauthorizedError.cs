using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    public class UnauthorizedError : ApiError
    {
        #region Class Variables

        private static readonly string _code = $"{(int)HttpStatusCode.Unauthorized} - {HttpStatusCode.Unauthorized}";

        #endregion

        #region Constructors

        public UnauthorizedError(Exception exception)
            : base(exception)
        {
        }

        public UnauthorizedError(string message, Exception exception = null)
            : base(_code, message, exception)
        {
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
