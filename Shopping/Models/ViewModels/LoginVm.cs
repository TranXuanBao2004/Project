using System.ComponentModel.DataAnnotations;

namespace Shopping.Models.ViewModels
{
    public class LoginVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter UserName")]
        public string UserName { get; set; }
        [DataType(DataType.Password),Required(ErrorMessage ="Please Enter Password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
