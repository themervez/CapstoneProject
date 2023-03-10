using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EntityLayer.Concrete
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal AccountAmount { get; set; }
        public string AccountIBAN { get; set; }
        public string BankBranch { get; set; }
        public bool Status { get; set; }

        public ICollection<Process> Processes { get; set; }
    }
}
