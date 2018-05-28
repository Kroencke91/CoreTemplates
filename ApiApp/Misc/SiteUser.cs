using ApiApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ApiApp.Misc
{
    public class SiteUser : ISiteUser
    {
        #region Class Variables
        #endregion

        #region Constructors

        public SiteUser(ClaimsPrincipal principal)
        {
            Principal = principal;
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Principal { get; }

        public IIdentity Identity => Principal.Identity;

        public string AuthenticationType => Principal.Identity.AuthenticationType;

        public bool IsAuthenticated => Principal.Identity.IsAuthenticated;

        public string Name => Principal.Identity.Name;

        public List<Claim> Claims => Principal.Claims.ToList();

        #endregion

        #region Public Methods

        public bool IsInRole(string role) => Principal.IsInRole(role);

        #endregion

        #region Private Methods
        #endregion
    }
}
