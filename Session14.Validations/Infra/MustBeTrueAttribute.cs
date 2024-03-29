﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Session14.Validations.Infra
{
    public class MustBeTrueAttribute : Attribute, IModelValidator
    {
        private bool _isRequired { get; set; }
        private string _errorMessage { get; set; }

        public MustBeTrueAttribute(bool isRequired = false, string errorMessage = "Must be true")
        {
            _isRequired = isRequired;
            _errorMessage = errorMessage;
        }
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            bool? value = context.Model as bool?;

            if (!value.HasValue || value.Value == false)
            {
                return new List<ModelValidationResult>{
                    new ModelValidationResult("", _errorMessage)
                };
            }
            return Enumerable.Empty<ModelValidationResult>();
        }
    }

}
