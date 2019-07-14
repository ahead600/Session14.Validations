using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Session14.Validations.Infra
{
    public class FileTypeAttribute : Attribute, IModelValidator
    {
        private readonly string _fileTypes;
        private string _errorMessage;


        public FileTypeAttribute(string fileTypes, string errorMessage = null)
        {
            _fileTypes = fileTypes.ToLower().Replace(" ", "");
            _errorMessage = errorMessage ?? $"Only File Type [ {_fileTypes} ] are acceptable";
        }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var model = context.Model;

            if (model==null) return Enumerable.Empty<ModelValidationResult>();
            
            var type = Path.GetExtension((context.Model as IFormFile).FileName).ToLower();
            var allowTypes = _fileTypes.Split(',');

            if (!allowTypes.Contains(type))
            {
                return new List<ModelValidationResult>{
                    new ModelValidationResult("", _errorMessage)
                };
            }
            return Enumerable.Empty<ModelValidationResult>();
        }
    }

}
