using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BankingApp.Presentation.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Email alanı gereklidir!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
