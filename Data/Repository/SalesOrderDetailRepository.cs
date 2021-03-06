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
            SalesOrderDetail detail = Find(sod => sod.Id == Id);
            if (detail != null) { detail.Errors = new Dictionary<string, string>(); }
            return detail;
        }

        public SalesOrderDetail CreateObject(SalesOrderDetail salesOrderDetail)
        {
            string ParentCode = "";
            using (var db = GetContext())
            {
                ParentCode = (from obj in db.SalesOrders
                              where obj.Id == salesOrderDetail.SalesOrderId
                              select obj.Code).FirstOrDefault();
            }
            salesOrderDetail.Code = SetObjectCode(ParentCode);
            salesOrderDetail.IsConfirmed = false;
            salesOrderDetail.IsFulfilled = false;
            salesOrderDetail.IsDeleted = false;
            salesOrderDetail.CreatedAt = DateTime.Now;
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

        public string SetObjectCode(string ParentCode)
        {
            // Code: #{parent_object.code}/#{total_number_objects}
            int totalobject = FindAll().Count() + 1;
            string Code = ParentCode + "/#" + totalobject;
            return Code;
        } 

    }
}