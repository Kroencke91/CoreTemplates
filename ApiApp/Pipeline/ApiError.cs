using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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

        public ApiError(string code, string message, ModelStateDictionary modelState)
        {
            try
            {
                Error = new ApiErrorInfo(code, message, modelState);
            }
            catch (Exception ex)
            {
                Error = new ApiErrorInfo
                {
                    Code = $"{ex.GetType().FullName}",

                    Message = ex.Message
                };
            }
        }

        public ApiError(Exception exception)
        {
            try
            {
                Error = new ApiErrorInfo(exception);
            }
            catch (Exception ex)
            {
                Error = new ApiErrorInfo
                {
                    Code = $"{ex.GetType().FullName}",

                    Message = ex.Message
                };
            }
        }

        public ApiError(string code, string message, Exception exception)
        {
            try
            {
                Error = new ApiErrorInfo() { Code = code, Message = message };

                if (exception != null)
                {
                    Error.InnerException = new ApiErrorInfo(exception);
                }
            }
            catch (Exception ex)
            {
                Error = new ApiErrorInfo
                {
                    Code = $"{ex.GetType().FullName}",

                    Message = ex.Message.Replace("\"", "'")
                };
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

            public ApiErrorInfo(string code, string message, ModelStateDictionary modelState)
            {
                Code = code;

                Message = message;

                Errors = modelState.Keys             
                                   .Where(key => !string.IsNullOrWhiteSpace(key))
                                   .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError( key, x.ErrorMessage)))
                                   .ToList();
            }

            public ApiErrorInfo(Exception exception)
            {
                Code = exception.GetType().FullName;

                Message = exception.Message;

                if (exception.Data.Count > 0)
                {
                    var sb = new StringBuilder();

                    foreach (DictionaryEntry entry in exception.Data)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(" | ");
                        }

                        sb.Append($"{entry.Key}: {entry.Value}");
                    }

                    InnerError = new InnerError(sb.ToString());
                }

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
            [JsonProperty("innerError", NullValueHandling = NullValueHandling.Ignore)]
            public InnerError InnerError { get; set; }

            [DataMember(EmitDefaultValue = false)]
            [JsonProperty("innerException", NullValueHandling = NullValueHandling.Ignore)]
            public ApiErrorInfo InnerException { get; set; }

            [DataMember(EmitDefaultValue = false)]
            [JsonProperty("validationErrors", NullValueHandling = NullValueHandling.Ignore)]
            public List<ValidationError> Errors { get; set; }

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

            public InnerError() { }

            public InnerError(string message)
            {
                Message = message;
            }

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
