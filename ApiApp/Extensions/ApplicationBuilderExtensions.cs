using ApiApp.Pipeline;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        #region Class Variables
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public Methods

        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResponseWrapper>();
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
