using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private IPurchaseOrderRepository _p;
        public PurchaseOrderService(IPurchaseOrderRepository _purchaseOrderRepository)
        {
            _p = _purchaseOrderRepository;
        }

        public IList<PurchaseOrder> GetAll()
        {
            return _p.GetAll();
        }

        public PurchaseOrder GetObjectById(int Id)
        {
            return _p.GetObjectById(Id);
        }

        public IList<PurchaseOrder> GetObjectsByContactId(int contactId)
        {
            return _p.GetObjectsByContactId(contactId);
        }

        public PurchaseOrder CreateObject(PurchaseOrder purchaseOrder)
        {
            return _p.CreateObject(purchaseOrder);
        }

        public PurchaseOrder CreateObject(int contactId, DateTime purchaseDate)
        {
            PurchaseOrder po = new PurchaseOrder
            {
                CustomerId = contactId,
                PurchaseDate = purchaseDate
            };
            return _p.CreateObject(po);
        }

        public PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder)
        {
            return _p.UpdateObject(purchaseOrder);
        }

        public PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder)
        {
            return _p.SoftDeleteObject(purchaseOrder);
        }

        public bool DeleteObject(int Id)
        {
            return _p.DeleteObject(Id);
        }

        public PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderDetailService _pods,
                                    IStockMutationService _stockMutationService, IItemService _itemService)
        {
            IList<PurchaseOrderDetail> details = _pods.GetObjectsByPurchaseOrderId(purchaseOrder.Id);
            foreach (var detail in details)
            {
                _pods.ConfirmObject(detail, _stockMutationService, _itemService);
                _pods.FulfilObject(detail, true);
            }
            return _p.ConfirmObject(purchaseOrder);
        }

        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderDetailService _pods,
                                    IStockMutationService _stockMutationService, IItemService _itemService)
        {
            IList<PurchaseOrderDetail> details = _pods.GetObjectsByPurchaseOrderId(purchaseOrder.Id);
            foreach (var detail in details)
            {
                _pods.UnconfirmObject(detail, _stockMutationService, _itemService);
                _pods.FulfilObject(detail, false);
            }
            return _p.UnconfirmObject(purchaseOrder);
        }
    }
}