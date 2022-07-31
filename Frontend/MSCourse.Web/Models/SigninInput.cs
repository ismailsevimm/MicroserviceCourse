using System.ComponentModel.DataAnnotations;

namespace MSCourse.Web.Models
{
    public class SigninInput
    {
        [Display(Name = "E-Posta adresiniz")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Şifreniz")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool IsRemember { get; set; }
    }
}
