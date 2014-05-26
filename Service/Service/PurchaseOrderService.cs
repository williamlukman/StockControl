using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
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
        private IPurchaseOrderValidator _validator;

        public PurchaseOrderService(IPurchaseOrderRepository _purchaseOrderRepository, IPurchaseOrderValidator _purchaseOrderValidator)
        {
            _p = _purchaseOrderRepository;
            _validator = _purchaseOrderValidator;
        }

        public IPurchaseOrderValidator GetValidator()
        {
            return _validator;
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
            }
            return _p.ConfirmObject(purchaseOrder);
        }

        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderDetailService _purchaseOrderDetailService,
                                    IPurchaseReceivalDetailService _purchaseReceivalDetailService, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            IList<PurchaseOrderDetail> details = _purchaseOrderDetailService.GetObjectsByPurchaseOrderId(purchaseOrder.Id);
            foreach (var detail in details)
            {
                _purchaseOrderDetailService.UnconfirmObject(detail, _purchaseReceivalDetailService, _stockMutationService, _itemService);
            }
            return _p.UnconfirmObject(purchaseOrder);
        }
    }
}