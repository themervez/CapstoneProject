using BankingApp.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Configuration.Validation.ProcessValidation
{
    public class ProcessValidator : AbstractValidator<Process>
    {
        public ProcessValidator()
        {
            RuleFor(x => x.ProcessName).NotEmpty().WithMessage("İşlem Adı alanı boş geçilemez!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("İşlem Açıklaması alanı boş geçilemez!");
            RuleFor(x => x.ProcessName).MinimumLength(2).WithMessage("Lütfen en az 4 karakter veri giriniz!");
            RuleFor(x => x.Description).MinimumLength(2).WithMessage("Lütfen en az 4 karakter veri giriniz!");
        }
    }
}
