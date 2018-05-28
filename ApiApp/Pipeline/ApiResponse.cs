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
        public string Version { get { return "1.2.3"; } } //TODO: Pull from request URL

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public string RequestUrl { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string requestUrl, object result = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;

            RequestUrl = requestUrl;

            Result = result;

            ErrorMessage = errorMessage;
        }
    }
}
