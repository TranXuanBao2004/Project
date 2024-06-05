using System.ComponentModel.DataAnnotations;

namespace Shopping.Models.ViewModels
{
    public class AppUserVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}
