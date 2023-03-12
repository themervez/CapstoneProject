using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Features.OptionsModels
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public string PasswordKey { get; set; }
        public string Email { get; set; }
    }
}
