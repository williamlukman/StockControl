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
    public class SalesOrderDetailRepository : EfRepository<SalesOrderDetail>, ISalesOrderDetailRepository
    {
        private StockControlEntities stocks;
        public SalesOrderDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<SalesOrderDetail> GetObjectsBySalesOrderId(int salesOrderId)
        {
            return FindAll(sod => sod.SalesOrderId == salesOrderId && !sod.IsDeleted).ToList();
        }

        public SalesOrderDetail GetObjectById(int Id)
        {
            return Find(sod => sod.Id == Id);
        }

        public SalesOrderDetail CreateObject(SalesOrderDetail salesOrderDetail)
        {
            salesOrderDetail.IsConfirmed = false;
            salesOrderDetail.IsFulfilled = false;
            salesOrderDetail.IsDeleted = false;
            salesOrderDetail.CreatedAt = DateTime.Now;
            salesOrderDetail.Errors = new HashSet<string>();
            return Create(salesOrderDetail);
        }

        public SalesOrderDetail UpdateObject(SalesOrderDetail salesOrderDetail)
        {
            salesOrderDetail.ModifiedAt = DateTime.Now;
            Update(salesOrderDetail);
            return salesOrderDetail;
        }

        public SalesOrderDetail SoftDeleteObject(SalesOrderDetail salesOrderDetail)
        {
            salesOrderDetail.IsDeleted = true;
            salesOrderDetail.DeletedAt = DateTime.Now;
            Update(salesOrderDetail);
            return salesOrderDetail;
        }

        public bool DeleteObject(int Id)
        {
            SalesOrderDetail sod = Find(x => x.Id == Id);
            return (Delete(sod) == 1) ? true : false;
        }

        public SalesOrderDetail ConfirmObject(SalesOrderDetail salesOrderDetail)
        {
            salesOrderDetail.IsConfirmed = true;
            salesOrderDetail.ConfirmedAt = DateTime.Now;
            Update(salesOrderDetail);
            return salesOrderDetail;
        }

        public SalesOrderDetail UnconfirmObject(SalesOrderDetail salesOrderDetail)
        {
            salesOrderDetail.IsConfirmed = false;
            Update(salesOrderDetail);
            return salesOrderDetail;
        }

        public SalesOrderDetail FulfilObject(SalesOrderDetail salesOrderDetail)
        {
            salesOrderDetail.IsFulfilled = true;
            Update(salesOrderDetail);
            return salesOrderDetail;
        }
    }
}