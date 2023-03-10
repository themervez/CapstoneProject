using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EntityLayer.Concrete
{
    public class Process
    {
        [Key]
        public int ID { get; set; }
        public string ProcessName { get; set; }
        public string ProcessBranch { get; set; }
        public string Description { get; set; }
        public int AppUserId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        //[ForeignKey("CustomerId")]
        //public Customer CustomerI { get; set; }

    }
}
