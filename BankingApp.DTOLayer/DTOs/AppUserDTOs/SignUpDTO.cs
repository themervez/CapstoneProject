using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.DTOLayer.DTOs.AppUserDTOs
{
    public class SignUpDTO
    {

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
