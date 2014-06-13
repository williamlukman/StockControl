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
    public class ReceivableRepository : EfRepository<Receivable>, IReceivableRepository
    {

        private StockControlEntities stocks;
        public ReceivableRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<Receivable> GetAll()
        {
            return FindAll().ToList();
        }

        public IList<Receivable> GetObjectsByContactId(int contactId)
        {
            return FindAll(r => r.ContactId == contactId && !r.IsDeleted).ToList();
        }

        public Receivable GetObjectBySource(string ReceivableSource, int ReceivableSourceId)
        {
            Receivable receivable = Find(r => r.ReceivableSource == ReceivableSource && r.ReceivableSourceId == ReceivableSourceId && !r.IsDeleted);
            if (receivable != null) { receivable.Errors = new Dictionary<string, string>(); }
            return receivable;
        }

        public Receivable GetObjectById(int Id)
        {
            Receivable receivable = Find(r => r.Id == Id && !r.IsDeleted);
            if (receivable != null) { receivable.Errors = new Dictionary<string, string>(); }
            return receivable;
        }

        public Receivable CreateObject(Receivable receivable)
        {
            receivable.PendingClearanceAmount = 0;
            receivable.IsCompleted = false;
            receivable.IsDeleted = false;
            receivable.CreatedAt = DateTime.Now;
            return Create(receivable);
        }

        public Receivable UpdateObject(Receivable receivable)
        {
            receivable.ModifiedAt = DateTime.Now;
            Update(receivable);
            return receivable;
        }

        public Receivable SoftDeleteObject(Receivable receivable)
        {
            receivable.IsDeleted = true;
            receivable.DeletedAt = DateTime.Now;
            Update(receivable);
            return receivable;
        }

        public bool DeleteObject(int Id)
        {
            Receivable receivable = Find(x => x.Id == Id);
            return (Delete(receivable) == 1) ? true : false;
        }
    }
}