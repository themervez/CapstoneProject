using System.ComponentModel.DataAnnotations;

namespace BankingApp.Presentation.Models
{
    public class SignUpViewModel
    {

        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen E-mail Adresinizi Giriniz")]
        [EmailAddress(ErrorMessage = "Lütfen Geçerli Bir E-mail Adresi Giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen Telefon Numaranızı Giriniz")]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Lütfen Şehir Bilginizi Giriniz")]
        public string City { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Tekrar Giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor. Lütfen tekrar Deneyiniz")]
        public string ConfirmPassword { get; set; }
    }
}
