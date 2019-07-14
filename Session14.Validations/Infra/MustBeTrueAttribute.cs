using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

    public static class Helpers
    {
        public static string ValidateNationalId(this String nationalId)
        {

            if (String.IsNullOrEmpty(nationalId))
                return "لطفا کد ملی را صحیح وارد نمایید";

            if (nationalId.Length != 10)
                return "طول کد ملی باید ده کاراکتر باشد";

            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalId))
                return "کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید";

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalId)) return "کد ملی قابل قبول نمی باشد";


            //عملیات شرح داده شده در بالا
            var chArray = nationalId.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            //return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
            if (!(((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)))) return "کد ملی قابل قبول نمی باشد";
            return null;
        }
    }

}
