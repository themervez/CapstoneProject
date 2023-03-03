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
    public class ProcessService : IProcessService
    {
        private readonly IProcessDAL _processDAL;

        public ProcessService(IProcessDAL processDAL)
        {
            _processDAL = processDAL;
        }

        public void TDelete(Process t)
        {
            _processDAL.Delete(t);
        }

        public Process TGetById(int id)
        {
            return _processDAL.GetById(id);
        }

        public List<Process> TGetList()
        {
            return _processDAL.GetList();
        }

        public void TInsert(Process t)
        {
            _processDAL.Insert(t);
        }

        public void TUpdate(Process t)
        {
           _processDAL.Update(t);
        }
    }
}
