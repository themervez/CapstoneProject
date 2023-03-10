using BankingApp.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Configuration.Validation.AppUserValidation
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş geçilemez!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-mail alanı boş geçilemez!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon bilgisi alanı boş geçilemez!");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri giriniz!");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri giriniz!");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter veri giriniz!");
            RuleFor(x => x.Surname).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter veri giriniz!");
            RuleFor(x => x.JoinDate).NotEmpty().WithMessage("Müşteri kayıt olma tarihi boş geçilemez!");
            RuleFor(x => x.BankBranch).NotEmpty().WithMessage("Banka Şubesi alanı boş geçilemez!");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("İş tanımı alanı boş geçilemez!");
            RuleFor(x => x.JobDescription).MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri giriniz!");
            RuleFor(x => x.BankBranch).NotEmpty().WithMessage("Banka Şubesi alanı boş geçilemez!");
        }
    }
}
