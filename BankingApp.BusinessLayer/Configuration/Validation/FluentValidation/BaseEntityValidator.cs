using BankingApp.EntityLayer.Concrete.Common;
using FluentValidation;

namespace BankingApp.BusinessLayer.Configuration.Validation.FluentValidation
{
    public class BaseEntityValidator : AbstractValidator<BaseEntity>
    {
        public BaseEntityValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş geçilemez!");
            RuleFor(x => x.BankBranch).NotEmpty().WithMessage("Banka Şubesi alanı boş geçilemez!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-mail alanı boş geçilemez!");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon bilgisi alanı boş geçilemez!");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri giriniz!");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri giriniz!");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter veri giriniz!");
            RuleFor(x => x.Surname).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter veri giriniz!");
        }
    }
}
