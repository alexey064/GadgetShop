using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Model
{
    public class DEL_LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
