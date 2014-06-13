using Core.DomainModel;
using Core.Interface.Repository;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SalesInvoiceRepository : EfRepository<SalesInvoice>, ISalesInvoiceRepository
    {
        private StockControlEntities stocks;
        public SalesInvoiceRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<SalesInvoice> GetAll()
        {
            return FindAll(si => !si.IsDeleted).ToList();
        }

        public SalesInvoice GetObjectById(int Id)
        {
            SalesInvoice salesInvoice = Find(si => si.Id == Id && !si.IsDeleted);
            if (salesInvoice != null) { salesInvoice.Errors = new Dictionary<string, string>(); }
            return salesInvoice;
        }

        public IList<SalesInvoice> GetObjectsByContactId(int contactId)
        {
            return FindAll(si => si.ContactId == contactId && !si.IsDeleted).ToList();
        }

        public SalesInvoice CreateObject(SalesInvoice salesInvoice)
        {
            salesInvoice.IsDeleted = false;
            salesInvoice.IsConfirmed = false;
            salesInvoice.CreatedAt = DateTime.Now;
            return Create(salesInvoice);
        }

        public SalesInvoice UpdateObject(SalesInvoice salesInvoice)
        {
            salesInvoice.ModifiedAt = DateTime.Now;
            Update(salesInvoice);
            return salesInvoice;
        }

        public SalesInvoice SoftDeleteObject(SalesInvoice salesInvoice)
        {
            salesInvoice.IsDeleted = true;
            salesInvoice.DeletedAt = DateTime.Now;
            Update(salesInvoice);
            return salesInvoice;
        }

        public bool DeleteObject(int Id)
        {
            SalesInvoice si = Find(x => x.Id == Id);
            return (Delete(si) == 1) ? true : false;
        }

        public SalesInvoice ConfirmObject(SalesInvoice salesInvoice)
        {
            salesInvoice.IsConfirmed = true;
            Update(salesInvoice);
            return salesInvoice;
        }

        public SalesInvoice UnconfirmObject(SalesInvoice salesInvoice)
        {
            salesInvoice.IsConfirmed = false;
            Update(salesInvoice);
            return salesInvoice;
        }
    }
}