using ApiApp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ApiApp.Models
{
    public class SampleModel : IValidatableObject
    {
        #region Class Variables
        #endregion

        #region Constructors
        #endregion

        #region Properties

        [Required]
        [StringLength(5)]
        public string Id { get; set; }

        #endregion

        #region Public Methods

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var err = new AppValidationResult(validationContext, $"Id must be an integer", new[] { "Id" });
                        
            var errMsgJson = JsonConvert.SerializeObject(err);

            yield return new ValidationResult(errMsgJson, new[] { "Id" });            
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
