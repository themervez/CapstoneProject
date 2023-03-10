using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EntityLayer.Concrete
{
    public class Employee
    {
        public int ID { get; set; }
        public string JobDescription { get; set; }
        public string BankBranch { get; set; }
        public bool Status { get; set; }
        public ICollection<Process> Processes { get; set; }
    }
}
