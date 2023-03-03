using BankingApp.EntityLayer.Concrete.Common;
using FluentValidation;

namespace BankingApp.BusinessLayer.Configuration.Validation.FluentValidation
{
    public class BaseEntityValidator : AbstractValidator<BaseEntity>
    {
        public BaseEntityValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad Soyad alanı boş geçilemez!");
            RuleFor(x => x.BankBranch).NotEmpty().WithMessage("Banka Şubesi alanı boş geçilemez!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-mail alanı boş geçilemez!");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon bilgisi alanı boş geçilemez!");
            RuleFor(x => x.NameSurname).MinimumLength(4).WithMessage("Lütfen en az 4 karakter veri giriniz!");
            RuleFor(x => x.NameSurname).MaximumLength(60).WithMessage("Lütfen en fazla 60 karakter veri giriniz!");
        }
    }
}
