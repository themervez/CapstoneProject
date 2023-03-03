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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDAL _customerDAL;

        public CustomerService(ICustomerDAL customerDAL)
        {
            _customerDAL = customerDAL;
        }

        public void TDelete(Customer t)
        {
            _customerDAL.Delete(t);
        }

        public Customer TGetById(int id)
        {
           return _customerDAL.GetById(id);
        }

        public List<Customer> TGetList()
        {
            return _customerDAL.GetList();
        }

        public void TInsert(Customer t)
        {
            _customerDAL.Insert(t);
        }

        public void TUpdate(Customer t)
        {
            _customerDAL.Update(t);
        }
    }
}
