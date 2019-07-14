using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Session14.Validations.Infra
{
    public class FileSizeAttribute : Attribute, IModelValidator
    {
        private readonly long _minSize;
        private readonly long _maxSize;
        private string _errorMessage;


        public FileSizeAttribute(long minSize = 1, long maxSize = 1024 * 1024, string errorMessage = null)
        {
            _minSize = minSize;
            _maxSize = maxSize;
            _errorMessage = errorMessage ?? $"File size Must be in {_minSize} to {_maxSize} bytes range";
        }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var model = context.Model;
            if (model != null)
            {
                long size = (model as IFormFile).Length;                

                if (size < _minSize || size > _maxSize)
                {
                    return new List<ModelValidationResult>{
                        new ModelValidationResult("", _errorMessage)
                    };
                }
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }

}
