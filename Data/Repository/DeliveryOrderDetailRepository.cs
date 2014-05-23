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
    public class DeliveryOrderDetailRepository : EfRepository<DeliveryOrderDetail>, IDeliveryOrderDetailRepository
    {
        private StockControlEntities stocks;
        public DeliveryOrderDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<DeliveryOrderDetail> GetObjectsByDeliveryOrderId(int deliveryOrderId)
        {
            return FindAll(prd => prd.DeliveryOrderId == deliveryOrderId && !prd.IsDeleted).ToList();
        }

        public DeliveryOrderDetail GetObjectById(int Id)
        {
            return Find(prd => prd.Id == Id);
        }

        public DeliveryOrderDetail CreateObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            deliveryOrderDetail.IsFulfilled = false;
            deliveryOrderDetail.IsConfirmed = false;
            deliveryOrderDetail.IsDeleted = false;
            deliveryOrderDetail.CreatedAt = DateTime.Now;
            deliveryOrderDetail.Errors = new HashSet<string>();
            return Create(deliveryOrderDetail);
        }

        public DeliveryOrderDetail UpdateObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            deliveryOrderDetail.ModifiedAt = DateTime.Now;
            Update(deliveryOrderDetail);
            return deliveryOrderDetail;
        }

        public DeliveryOrderDetail SoftDeleteObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            deliveryOrderDetail.IsDeleted = true;
            deliveryOrderDetail.DeletedAt = DateTime.Now;
            Update(deliveryOrderDetail);
            return deliveryOrderDetail;
        }

        public bool DeleteObject(int Id)
        {
            DeliveryOrderDetail prd = Find(x => x.Id == Id);
            return (Delete(prd) == 1) ? true : false;
        }

        public DeliveryOrderDetail ConfirmObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            deliveryOrderDetail.IsConfirmed = true;
            deliveryOrderDetail.ConfirmedAt = DateTime.Now;
            Update(deliveryOrderDetail);
            return deliveryOrderDetail;
        }

        public DeliveryOrderDetail UnconfirmObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            deliveryOrderDetail.IsConfirmed = false;
            Update(deliveryOrderDetail);
            return deliveryOrderDetail;
        }

        public DeliveryOrderDetail FulfilObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            deliveryOrderDetail.IsFulfilled = true;
            Update(deliveryOrderDetail);
            return deliveryOrderDetail;
        }
    }
}