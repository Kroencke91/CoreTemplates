using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Validation
{
    public class SampleModelValidationResult : AppValidationResult
    {
        #region Class Variables
        #endregion

        #region Constructors

        public SampleModelValidationResult(ValidationContext validationContext, string errorMessage)
            : base(validationContext, errorMessage)
        {
        }

        public SampleModelValidationResult(ValidationContext validationContext, string errorMessage, IEnumerable<string> memberNames)
            : base(validationContext, errorMessage, memberNames)
        {
        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
