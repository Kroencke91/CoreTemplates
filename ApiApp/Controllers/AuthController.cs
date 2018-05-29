
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using System.Text;

using ApiApp.Extensions;
using ApiApp.Interfaces;
using ApiApp.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using ApiApp.Pipeline;

namespace ApiApp.Controllers.V_1_0
{
    [Produces("application/json")]
    [ApiVersion(CV.ApiVersions.V_1_0)]
    [Route("api/v{version:apiVersion}")]
    public sealed class AuthController : ControllerBase
    {
        #region Class Variables

        private IAppSecurity _security;

        #endregion

        #region Constructors

        public AuthController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
            _security = AppInfo.AppSecurity;
        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods

        [HttpGet("[action]")]
        public IActionResult Error()
        {
            return Ok($"Error - {AppInfo.ApplicationName} - {AppInfo.EnvironmentName} - {DateTime.Now}");
        }

        [HttpPost("[action]")]
        public IActionResult Authenticate()
        {
            var (Token, ErrMsg) = Request.BasicToken();

            if (!string.IsNullOrEmpty(ErrMsg))
            {
                return Unauthorized(new UnauthorizedError(ErrMsg));
            }

            var identity = _security.GetClaimsIdentityFromTokenAsync(MemoryCache, Base64Decode(Token));

            if (identity.Claims.Count() == 0)
            {
                return Unauthorized(new UnauthorizedError("Authorization Failed"));
            }

            var token = _security.GenerateToken(identity);

            return new JsonResult(new AuthToken(token));
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
