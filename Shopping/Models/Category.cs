using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class Category
    {
       
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Please enter the correct name as required")]
        public  string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Please enter the correct description as required")]
        public string Description { get; set; }
        
        public string Slug { get; set; }

        public  int Status { get; set; }
        public List<Product> Products { get; set; }
        
    }
}
