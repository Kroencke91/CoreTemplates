using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    [DataContract]
    public class ApiResponse
    {
        [DataMember]
        [JsonProperty("apiVersion")]
        public string ApiVersion { get; }

        [DataMember]
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; }

        [DataMember(EmitDefaultValue = false)]
        [JsonProperty("statusMessage")]
        public string StatusMessage { get; }

        [DataMember]
        [JsonProperty("requestUrl")]
        public string RequestUrl { get; }

        [DataMember(EmitDefaultValue = false)]
        [JsonProperty("result")]
        public object Result { get; }

        public ApiResponse(HttpStatusCode statusCode,
                           string statusMessage,
                           string requestUrl, 
                           string apiVersion, 
                           object result = null)
        {
            StatusCode = statusCode;

            StatusMessage = statusMessage;

            ApiVersion = apiVersion;

            RequestUrl = requestUrl;

            Result = result;
        }
    }
}
