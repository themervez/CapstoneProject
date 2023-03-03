using BankingApp.EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EntityLayer.Concrete
{
    public class Employee: BaseEntity
    {
        public string JobDescription { get; set; }
        public ICollection<Process> Processes { get; set; }
    }
}
