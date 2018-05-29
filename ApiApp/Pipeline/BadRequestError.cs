using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    public class BadRequestError : ApiError
    {
        #region Class Variables

        private static readonly string _code = $"{(int)HttpStatusCode.BadRequest} - {HttpStatusCode.BadRequest}";

        #endregion

        #region Constructors

        public BadRequestError(Exception exception)
            : base(exception)
        {
        }

        public BadRequestError(string message, Exception exception = null)
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
