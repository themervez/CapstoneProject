using BankingApp.BusinessLayer.Features.Abstract;
using BankingApp.DAL.Abstract;
using BankingApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Features.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDAL _employeeDAL;

        public EmployeeService(IEmployeeDAL employeeDAL)
        {
            _employeeDAL = employeeDAL;
        }

        public void TDelete(Employee t)
        {
            _employeeDAL.Delete(t);
        }

        public Employee TGetById(int id)
        {
            return _employeeDAL.GetById(id);
        }

        public List<Employee> TGetList()
        {
            return _employeeDAL.GetList();
        }

        public void TInsert(Employee t)
        {
            _employeeDAL.Insert(t);
        }

        public void TUpdate(Employee t)
        {
            _employeeDAL.Update(t);
        }
    }
}
