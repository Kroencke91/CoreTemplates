using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ApiApp.Interfaces
{
    //Customize SiteUser by application
    public interface ISiteUser 
    {
        #region Properties

        ClaimsPrincipal Principal { get; }

        IIdentity Identity { get; }

        string AuthenticationType { get; }

        bool IsAuthenticated { get; }

        string Name { get; }

        List<Claim> Claims { get; }

        #endregion

        #region Public Methods

        bool IsInRole(string role);

        #endregion
    }
}
