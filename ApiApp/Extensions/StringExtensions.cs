using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.String;

namespace ApiApp.Extensions
{
    public static  class StringExtensions
    {
        #region Class Variables
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public Methods

        public static string NullIfEmpty(this string value) => IsNullOrEmpty(value) ? null : value;

        #endregion

        #region Private Methods
        #endregion
    }
}
