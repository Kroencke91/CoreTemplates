using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models
{
    public class BVSafeSitesQuery
    {
        #region Class Variables
        #endregion

        #region Constructors
        #endregion

        #region Properties

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("modifiedAfter")]
        public DateTime ModifiedAfter { get; set; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
