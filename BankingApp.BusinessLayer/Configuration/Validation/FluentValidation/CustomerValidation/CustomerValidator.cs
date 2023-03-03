using BankingApp.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Configuration.Validation.FluentValidation.CustomerValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.JoinDate).NotEmpty().WithMessage("Müşteri kayıt olma tarihi boş geçilemez!");
        }
    }
}
