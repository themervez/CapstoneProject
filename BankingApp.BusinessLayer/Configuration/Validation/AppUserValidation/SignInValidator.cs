using BankingApp.DTOLayer.DTOs.AppUserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Configuration.Validation.AppUserValidation
{
    public class SignInValidator : AbstractValidator<SignInDTO>
    {
        public SignInValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı alanı boş geçilemez!")
                        .MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri giriniz!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez!");
        }
    }
}
