using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiApp.Interfaces
{
    public interface IAppSecurity
    {
        #region Properties

        SymmetricSecurityKey IssuerSigningKey { get; }

        SigningCredentials SigningCredentials { get; }

        #endregion

        #region Public Methods

        ClaimsIdentity GetClaimsIdentityFromToken(string token);

        string GenerateToken(ClaimsIdentity claimsIdentity);

        #endregion
    }
}
