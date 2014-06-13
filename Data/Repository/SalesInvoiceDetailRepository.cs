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
    public class SalesInvoiceDetailRepository : EfRepository<SalesInvoiceDetail>, ISalesInvoiceDetailRepository
    {
        private StockControlEntities stocks;
        public SalesInvoiceDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<SalesInvoiceDetail> GetObjectsBySalesInvoiceId(int salesInvoiceId)
        {
            return FindAll(sid => sid.SalesInvoiceId == salesInvoiceId && !sid.IsDeleted).ToList();
        }

        public SalesInvoiceDetail GetObjectById(int Id)
        {
            SalesInvoiceDetail detail = Find(sid => sid.Id == Id);
            if (detail != null) { detail.Errors = new Dictionary<string, string>(); }
            return detail;
        }

        public SalesInvoiceDetail CreateObject(SalesInvoiceDetail salesInvoiceDetail)
        {
            salesInvoiceDetail.IsConfirmed = false;
            salesInvoiceDetail.IsDeleted = false;
            salesInvoiceDetail.CreatedAt = DateTime.Now;
            return Create(salesInvoiceDetail);
        }

        public SalesInvoiceDetail UpdateObject(SalesInvoiceDetail salesInvoiceDetail)
        {
            salesInvoiceDetail.ModifiedAt = DateTime.Now;
            Update(salesInvoiceDetail);
            return salesInvoiceDetail;
        }

        public SalesInvoiceDetail SoftDeleteObject(SalesInvoiceDetail salesInvoiceDetail)
        {
            salesInvoiceDetail.IsDeleted = true;
            salesInvoiceDetail.DeletedAt = DateTime.Now;
            Update(salesInvoiceDetail);
            return salesInvoiceDetail;
        }

        public bool DeleteObject(int Id)
        {
            SalesInvoiceDetail sid = Find(x => x.Id == Id);
            return (Delete(sid) == 1) ? true : false;
        }

        public SalesInvoiceDetail ConfirmObject(SalesInvoiceDetail salesInvoiceDetail)
        {
            salesInvoiceDetail.IsConfirmed = true;
            Update(salesInvoiceDetail);
            return salesInvoiceDetail;
        }

        public SalesInvoiceDetail UnconfirmObject(SalesInvoiceDetail salesInvoiceDetail)
        {
            salesInvoiceDetail.IsConfirmed = false;
            Update(salesInvoiceDetail);
            return salesInvoiceDetail;
        }
    }
}