using System.ComponentModel.DataAnnotations;

namespace Shopping.Helpers
{
    public class FileExtensionAttribute:ValidationAttribute
    {
        public string[] AllowedExtensions { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!AllowedExtensions.Contains(extension))
                {
                    return new ValidationResult($"The UploadImage field only accepts files with the following extensions: {string.Join(", ", AllowedExtensions)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
