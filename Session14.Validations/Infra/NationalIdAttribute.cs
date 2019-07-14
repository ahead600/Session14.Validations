using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Session14.Validations.Infra
{
    public class NationalIdAttribute : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var value = context.Model as string;

            var _errorMessage = Helpers.ValidateNationalId(value);
            if (_errorMessage != null)
            {
                return new List<ModelValidationResult>{
                    new ModelValidationResult("", _errorMessage)
                };
            }
            return Enumerable.Empty<ModelValidationResult>();
        }
    }

}
