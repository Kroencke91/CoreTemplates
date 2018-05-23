using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Misc
{
    public class JwtInfo
    {
        #region Class Variables
        #endregion

        #region Constructors

        public JwtInfo(string issuer, string audience)
        {
            Issuer = issuer;

            Audience = audience;
        }

        #endregion

        #region Properties

        public string Issuer { get; }

        public string Audience { get; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
