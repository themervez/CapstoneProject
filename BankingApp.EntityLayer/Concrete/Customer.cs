using BankingApp.EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EntityLayer.Concrete
{
    public class Customer:BaseEntity
    {
        public DateTime JoinDate { get; set; }
        public decimal AccountAmount { get; set; }
        public string AccountIBAN { get; set; }
        public ICollection<Process> Processes { get; set; }
    }
}
