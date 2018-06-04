using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Validation
{
    public class AppValidationResult : ValidationResult
    {
        #region Class Variables
        #endregion

        #region Constructors

        public AppValidationResult() :base(string.Empty) { }

        public AppValidationResult(ValidationContext validationContext, string errorMessage)
            : base(errorMessage)
        {
            Initialize(validationContext);
        }

        public AppValidationResult(ValidationContext validationContext, string errorMessage, IEnumerable<string> memberNames)
            : base(errorMessage, memberNames)
        {
            Initialize(validationContext);
        }

        #endregion

        #region Properties

        public string ModelName { get; set; }

        public object SubmittedValue { get; set; }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        protected void Initialize(ValidationContext validationContext)
        {
            ModelName = validationContext.DisplayName;

            SubmittedValue = ((Models.SampleModel)validationContext.ObjectInstance).Id;
        }

        #endregion
    }
}
