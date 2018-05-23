
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

namespace ApiApp.Controllers.V_1_0
{
    [Produces("application/json")]
    [ApiVersion(CV.ApiVersions.V_1_0)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public sealed class AuthController : ControllerBase
    {
        #region Class Variables

        private IAppSecurity _security;

        #endregion

        #region Constructors

        public AuthController(IAppInfo appInfo) 
            : base(appInfo)
        {
            _security = appInfo.AppSecurity;
        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods

        [HttpPost]
        public JsonResult Authenticate()
        {
            var basicToken = Request.BasicToken();

            if (!string.IsNullOrEmpty(basicToken.ErrMsg))
            {
                Response.StatusCode = 401;

                return new JsonResult(new AuthToken(basicToken.ErrMsg));
            }
            
            var identity = _security.GetClaimsIdentityFromToken(Base64Decode(basicToken.Token));

            if (identity.Claims.Count() == 0)
            {
                Response.StatusCode = 401;

                return new JsonResult(new AuthToken("Authorization Failed"));
            }

            var token = _security.GenerateToken(identity);

            return new JsonResult(new AuthToken(token));
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
