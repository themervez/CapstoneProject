using BankingApp.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Configuration.Validation.FluentValidation.ProcessValidation
{
    public class ProcessValidator : AbstractValidator<Process>
    {
        public ProcessValidator()
        {
        }
    }
}
