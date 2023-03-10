using BankingApp.DTOLayer.DTOs.AppUserDTOs;
using BankingApp.DTOLayer.DTOs.CustomerDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Configuration.Validation.CustomerValidation
{
    public class ProfileUpdateValidator : AbstractValidator<ProfileUpdateDTO>
    {
        public ProfileUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez!")
                    .Length(2, 30).WithMessage("Lütfen 2 ile 30 karakter arasında veri giriniz!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş geçilemez!")
                                   .Length(2, 30).WithMessage("Lütfen 2 ile 30 karakter arasında veri giriniz!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı alanı boş geçilemez!")
                                    .MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri giriniz!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-mail alanı boş geçilemez!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez!");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre Onay alanı boş geçilemez!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon bilgisi alanı boş geçilemez!");
            RuleFor(x => x.Password).Equal(y => y.ConfirmPassword).WithMessage("Şifreler birbiriyle uyuşmuyor!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen Geçerli Bir E-mail Adresi Giriniz!").When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}

