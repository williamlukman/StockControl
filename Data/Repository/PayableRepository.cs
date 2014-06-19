using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Data.Repository;

namespace Data.Repository
{
    public class PayableRepository : EfRepository<Payable>, IPayableRepository
    {

        private StockControlEntities stocks;
        public PayableRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<Payable> GetAll()
        {
            return FindAll().ToList();
        }

        public IList<Payable> GetObjectsByContactId(int contactId)
        {
            return FindAll(p => p.ContactId == contactId && !p.IsDeleted).ToList();
        }

        public Payable GetObjectBySource(string PayableSource, int PayableSourceId)
        {
            Payable payable = Find(p => p.PayableSource == PayableSource && p.PayableSourceId == PayableSourceId && !p.IsDeleted);
            if (payable != null) { payable.Errors = new Dictionary<string, string>(); }
            return payable;
        }

        public Payable GetObjectById(int Id)
        {
            Payable payable = Find(p => p.Id == Id && !p.IsDeleted);
            if (payable != null) { payable.Errors = new Dictionary<string, string>(); }
            return payable;
        }

        public Payable CreateObject(Payable payable)
        {
            payable.Code = SetObjectCode();
            payable.PendingClearanceAmount = 0;
            payable.IsCompleted = false;
            payable.IsDeleted = false;
            payable.CreatedAt = DateTime.Now;
            return Create(payable);
        }

        public Payable UpdateObject(Payable payable)
        {
            payable.ModifiedAt = DateTime.Now;
            Update(payable);
            return payable;
        }

        public Payable SoftDeleteObject(Payable payable)
        {
            payable.IsDeleted = true;
            payable.DeletedAt = DateTime.Now;
            Update(payable);
            return payable;
        }

        public bool DeleteObject(int Id)
        {
            Payable payable = Find(x => x.Id == Id);
            return (Delete(payable) == 1) ? true : false;
        }

        public string SetObjectCode()
        {
            // Code: #{year}/#{total_number
            int totalobject = FindAll().Count() + 1;
            string Code = "#" + DateTime.Now.Year.ToString() + "/#" + totalobject;
            return Code;
        }
    }
}