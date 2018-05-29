using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Extensions
{
    public static class HttpRequestExtensions
    {
        #region Class Variables

        private const string AUTH_HEADER_PARSE_ERROR = "Missing or malformed Authorization header.";

        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

        #region Public Methods

        public static string Authorization(this HttpRequest request)
        {
            return request.Headers["Authorization"];
        }

        public static (string Token, string ErrMsg) BasicToken(this HttpRequest request)
        {
            return GetTokenValue(request.Headers);
        }

        public static (string Token, string ErrMsg) BearerToken(this HttpRequest request)
        {
            return GetTokenValue(request.Headers);
        }

        #endregion

        #region Private Methods

        private static (string Token, string ErrMsg) GetTokenValue(IHeaderDictionary headers)
        {
            var token = string.Empty;

            try
            {
                var authHeader = headers["Authorization"];

                if (authHeader.Any())
                {
                    var parts = authHeader.First().Split(' ');

                    if (parts.Length == 2)
                    {
                        token = parts[1];
                    }
                }

                return (token, string.IsNullOrWhiteSpace(token) ? AUTH_HEADER_PARSE_ERROR : "");
            }
            catch (Exception ex)
            {
                //TODO: Logging?

                return (token, ex.Message);
            }
        }

        #endregion
    }
}
