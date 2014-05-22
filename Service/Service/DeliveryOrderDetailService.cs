using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DeliveryOrderDetailService : IDeliveryOrderDetailService
    {
        private IDeliveryOrderDetailRepository _dod;
        public DeliveryOrderDetailService(IDeliveryOrderDetailRepository _deliveryOrderDetailRepository)
        {
            _dod = _deliveryOrderDetailRepository;
        }

        public IList<DeliveryOrderDetail> GetObjectsByDeliveryOrderId(int deliveryOrderId)
        {
            return _dod.GetObjectsByDeliveryOrderId(deliveryOrderId);
        }

        public DeliveryOrderDetail GetObjectById(int Id)
        {
            return _dod.GetObjectById(Id);
        }

        public DeliveryOrderDetail CreateObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.CreateObject(deliveryOrderDetail);
        }

        public DeliveryOrderDetail UpdateObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.UpdateObject(deliveryOrderDetail);
        }

        public DeliveryOrderDetail SoftDeleteObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.SoftDeleteObject(deliveryOrderDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _dod.DeleteObject(Id);
        }

        public DeliveryOrderDetail ConfirmObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.ConfirmObject(deliveryOrderDetail);
        }

        public DeliveryOrderDetail UnconfirmObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.UnconfirmObject(deliveryOrderDetail);
        }

        public DeliveryOrderDetail FulfilObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.FulfilObject(deliveryOrderDetail);
        }
    }
}