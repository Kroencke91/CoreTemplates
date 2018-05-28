
using Newtonsoft.Json;

namespace ApiApp.Misc
{
    public class AuthToken
    {
        #region Class Variables
        #endregion

        #region Constructors

        public AuthToken(string token)
        {
            Token = token;
        }

        #endregion

        #region Properties

        [JsonProperty("token")]
        public string Token { get; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
