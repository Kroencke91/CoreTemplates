using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Misc
{
    public class NullException : ApplicationException
    {
        #region Class Variables
        #endregion

        #region Constructors

        public NullException()
            : base("The ExceptionContext.Exception is null")
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
