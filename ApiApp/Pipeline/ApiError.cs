using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ApiApp.Pipeline
{
    [DataContract]
    public class ApiError
    {
        #region Class Variables
        #endregion

        #region Constructors

        public ApiError() { }

        public ApiError(Exception exception)
        {
            Error = new ApiErrorInfo(exception);
        }

        public ApiError(string code, string message, Exception exception)
        {
            Error = new ApiErrorInfo() { Code = code, Message = message };

            if (exception != null)
            {
                Error.InnerException = new ApiErrorInfo(exception);
            }
        }

        #endregion

        #region Properties

        [DataMember]
        [JsonProperty("error")]
        public ApiErrorInfo Error { get; set; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Nested Class

        [DataContract]
        public class ApiErrorInfo
        {
            #region Class Variables
            #endregion

            #region Constructors

            public ApiErrorInfo() { }

            public ApiErrorInfo(Exception exception)
            {
                Code = exception.GetType().FullName;

                Message = exception.Message;

                if (exception.InnerException != null)
                {
                    InnerException = new ApiErrorInfo(exception.InnerException);
                }
            }

            #endregion

            #region Properties

            [DataMember]
            [JsonProperty("code")]
            public string Code { get; set; }

            [DataMember]
            [JsonProperty("message")]
            public string Message { get; set; }

            [DataMember(EmitDefaultValue = false)]
            [JsonProperty("innerError")]
            public InnerError InnerError { get; set; }

            [DataMember(EmitDefaultValue = false)]
            [JsonProperty("innerException")]
            public ApiErrorInfo InnerException { get; set; }

            #endregion

            #region Public Methods
            #endregion

            #region Private Methods
            #endregion
        }

        //From https://github.com/Microsoft/aspnet-api-versioning : DefaultErrorResponseProvider.cs : Microsoft.AspNetCore.Mvc.Versioning.DefaultErrorResponseProvider
        [DataContract]
        public class InnerError
        {
            #region Class Variables
            #endregion

            #region Constructors
            #endregion

            #region Properties

            [DataMember]
            [JsonProperty("message")]
            public string Message { get; set; }

            #endregion

            #region Public Methods
            #endregion

            #region Private Methods
            #endregion
        }

        #endregion
    }
}
