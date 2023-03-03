using BankingApp.DAL.Abstract;
using BankingApp.DAL.DbContexts;
using BankingApp.DAL.Repository;
using BankingApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.DAL.EF
{
    public class EFEmployeeDAL : GenericRepository<Employee>, IEmployeeDAL
    {
        public EFEmployeeDAL(BankingAppDbContext context) : base(context)
        {
        }
    }
}
