using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageURL { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string JobDescription { get; set; }
        public string BankBranch { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal AccountAmount { get; set; }
        public string AccountIBAN { get; set; }
        public bool Status { get; set; }
        public string EmailCode { get; set; }

        public ICollection<Process> Processes { get; set; }
    }
}
