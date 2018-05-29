using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    public class ForbiddenError : ApiError
    {
        #region Class Variables

        private static readonly string _code = $"{(int)HttpStatusCode.Forbidden} - {HttpStatusCode.Forbidden}";

        #endregion

        #region Constructors

        public ForbiddenError(Exception exception)
            : base(exception)
        {
        }

        public ForbiddenError(string message, Exception exception = null)
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
