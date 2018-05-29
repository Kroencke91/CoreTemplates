using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    [DataContract]
    public class ValidationError
    {
        #region Class Variables
        #endregion

        #region Constructors

        public ValidationError() { }

        public ValidationError(string field, string message)
        {
            Field = string.IsNullOrEmpty(field) ? null : field;

            Message = message;
        }

        #endregion

        #region Properties

        [DataMember]
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }

        [DataMember]
        [JsonProperty("message")]
        public string Message { get; set; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
