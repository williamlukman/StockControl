using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Data.Repository;
using System.Data;

namespace Data.Repository
{
    public class CashBankRepository : EfRepository<CashBank>, ICashBankRepository
    {

        private StockControlEntities stocks;
        public CashBankRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<CashBank> GetAll()
        {
            return FindAll().ToList();
        }

        public CashBank GetObjectById(int Id)
        {
            CashBank cb = Find(x => x.Id == Id && !x.IsDeleted);
            if (cb != null) { cb.Errors = new Dictionary<string, string>(); }
            return cb;
        }

        public CashBank GetObjectByName(string Name)
        {
            CashBank cb = Find(x => x.Name == Name && !x.IsDeleted);
            if (cb != null) { cb.Errors = new Dictionary<string, string>(); }
            return cb;
        }

        public CashBank CreateObject(CashBank cashbank)
        {
            cashbank.IsDeleted = false;
            cashbank.CreatedAt = DateTime.Now;
            return Create(cashbank);
        }

        public CashBank UpdateObject(CashBank cashbank)
        {
            cashbank.ModifiedAt = DateTime.Now;
            Update(cashbank);
            return cashbank;
        }

        public CashBank SoftDeleteObject(CashBank cashbank)
        {
            cashbank.IsDeleted = true;
            cashbank.DeletedAt = DateTime.Now;
            Update(cashbank);
            return cashbank;
        }

        public bool DeleteObject(int Id)
        {
            CashBank cashbank = Find(x => x.Id == Id);           
            return (Delete(cashbank) == 1) ? true : false;
        }
        
    }
}