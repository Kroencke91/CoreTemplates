using ApiApp.Validation;
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
            //ModelName = modelName;

            var x = JsonConvert.DeserializeObject<AppValidationResult>(message);

            ModelName = x.ModelName;

            Field = string.IsNullOrEmpty(field) ? null : field;

            Message = x.ErrorMessage; //message;

            SubmittedValue = x.SubmittedValue;
        }

        public ValidationError(string modelName, string field, string message, object submittedValue)
        {
            ModelName = modelName;

            Field = string.IsNullOrEmpty(field) ? null : field;

            Message = message;

            //SubmittedValue = submittedValue;
        }

        #endregion

        #region Properties

        [DataMember]
        [JsonProperty("modelName")]
        public string ModelName { get; set; }

        [DataMember]
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }

        [DataMember]
        [JsonProperty("message")]
        public string Message { get; set; }

        [DataMember]
        [JsonProperty("submittedValue")]
        public object SubmittedValue { get; set; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
