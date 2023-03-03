using BankingApp.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Configuration.Validation.FluentValidation.EmployeeValidation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("İş tanımı alanı boş geçilemez!");
            RuleFor(x => x.JobDescription).MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri giriniz!");
        }
    }
}
