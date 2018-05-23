
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using ApiApp.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ApiApp.Misc
{
    public class AppSecurity : IAppSecurity
    {
        #region Class Variables

        private static string _issuer;

        private static string _audience;

        private IAppConfiguration _config;

        #endregion

        #region Constructors

        public AppSecurity(IAppConfiguration config)
        {
            if (SigningCredentials != null) throw new ApplicationException("AppSecurity should only be instantiated in Startup.");

            _config = config;

            InitializeSigningCredentials();

            _issuer = _config.JwtInfo.Issuer;

            _audience = _config.JwtInfo.Audience;
        }

        #endregion

        #region Properties

        public SymmetricSecurityKey IssuerSigningKey { get; private set; }

        public SigningCredentials SigningCredentials { get; private set; }

        #endregion

        #region Public Methods

        public ClaimsIdentity GetClaimsIdentityFromToken(string token)
        {
            var identity = new ClaimsIdentity();

            //TODO: Use token to validate request & add claims to identity
            var claim = new Claim("UserId", "9876");

            identity.AddClaim(claim);

            return identity;
        }

        public string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var token = string.Empty;

            var now = DateTime.Now;

            var handler = new JwtSecurityTokenHandler();

            var tempToken = handler.CreateJwtSecurityToken(_issuer, _audience, claimsIdentity, now,
                                                           now.Add(GetTokenEpirationDuration()), now,
                                                           SigningCredentials);

            token = handler.WriteToken(tempToken);

            return token;
        }

        #endregion

        #region Private Methods

        private void InitializeSigningCredentials()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetKeyForSymmetricSecurity()));

            SigningCredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        private string GetKeyForSymmetricSecurity()
        {
            var key = string.Empty;

            //TODO: Get key from database or elsewhere
            key = "e8cc0845-99dc-4ff1-8030-52d33b6d7a26-61d66ec2-eb48-4b86-8a9c-3c913cfbdf94"; //TODO: Replace!!!!

            return key;
        }

        private TimeSpan GetTokenEpirationDuration()
        {
            return TimeSpan.FromHours(1); //TODO: GetTokenEpirationDuration()
        }

        #endregion
    }
}
