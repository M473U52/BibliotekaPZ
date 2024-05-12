using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Biblioteka.Areas.Identity.Pages.Account.Validators
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var file = value as IFormFile;

            if (file == null)
                return ValidationResult.Success;

            if (file.Length > _maxFileSize)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var file = value as IFormFile;

            if (file == null)
                return ValidationResult.Success;

            var extension = Path.GetExtension(file.FileName);

            if (!_extensions.Contains(extension.ToLower()))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
