using System.ComponentModel.DataAnnotations;

namespace MKT.Website.UI.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = "UserName ")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email ")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password not matched")]
        public string ConfirmPassword { get; set; }
    }
}
