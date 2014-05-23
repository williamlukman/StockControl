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
    public class SalesOrderRepository : EfRepository<SalesOrder>, ISalesOrderRepository
    {
        private StockControlEntities stocks;
        public SalesOrderRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<SalesOrder> GetAll()
        {
            return FindAll(so => !so.IsDeleted).ToList();
        }

        public SalesOrder GetObjectById(int Id)
        {
            return Find(so => so.Id == Id && !so.IsDeleted);
        }

        public IList<SalesOrder> GetObjectsByContactId(int contactId)
        {
            return FindAll(so => so.CustomerId == contactId && !so.IsDeleted).ToList();
        }

        public SalesOrder CreateObject(SalesOrder salesOrder)
        {
            salesOrder.IsDeleted = false;
            salesOrder.IsConfirmed = false;
            salesOrder.CreatedAt = DateTime.Now;
            salesOrder.Errors = new HashSet<string>();
            return Create(salesOrder);
        }

        public SalesOrder UpdateObject(SalesOrder salesOrder)
        {
            salesOrder.ModifiedAt = DateTime.Now;
            Update(salesOrder);
            return salesOrder;
        }

        public SalesOrder SoftDeleteObject(SalesOrder salesOrder)
        {
            salesOrder.IsDeleted = true;
            salesOrder.DeletedAt = DateTime.Now;
            Update(salesOrder);
            return salesOrder;
        }

        public bool DeleteObject(int Id)
        {
            SalesOrder so = Find(x => x.Id == Id);
            return (Delete(so) == 1) ? true : false;
        }

        public SalesOrder ConfirmObject(SalesOrder salesOrder)
        {
            salesOrder.IsConfirmed = true;
            salesOrder.ConfirmedAt = DateTime.Now;
            Update(salesOrder);
            return salesOrder;
        }

        public SalesOrder UnconfirmObject(SalesOrder salesOrder)
        {
            salesOrder.IsConfirmed = false;
            Update(salesOrder);
            return salesOrder;
        }
    }
}