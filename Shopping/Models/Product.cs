using Shopping.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required,MinLength(4,ErrorMessage = "Please enter the correct name as required")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Please enter the correct description as required")]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required,Range(1,int.MaxValue,ErrorMessage = "Please select a brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Images { get; set; }
        [NotMapped]
        [FileExtension(AllowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile UploadImage { get; set; }
    }
}
