using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Extensions
{
    public static class ApiVersionExtensions
    {
        #region Class Variables
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public Methods

        public static ApiVersion CreateApiVersion(string value)
        {
            var parts = value.Split('.');

            var major = Convert.ToInt32(parts[0]);

            var minor = Convert.ToInt32(parts[1]);

            return new ApiVersion(major, minor);
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
