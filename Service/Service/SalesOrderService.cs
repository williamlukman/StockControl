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
    public class SalesOrderService : ISalesOrderService
    {
        private ISalesOrderRepository _s;
        private ISalesOrderValidator _validator;

        public SalesOrderService(ISalesOrderRepository _salesOrderRepository, ISalesOrderValidator _salesOrderValidator)
        {
            _s = _salesOrderRepository;
            _validator = _salesOrderValidator;
        }

        public ISalesOrderValidator GetValidator()
        {
            return _validator;
        }

        public IList<SalesOrder> GetAll()
        {
            return _s.GetAll();
        }

        public SalesOrder GetObjectById(int Id)
        {
            return _s.GetObjectById(Id);
        }

        public IList<SalesOrder> GetObjectsByContactId(int contactId)
        {
            return _s.GetObjectsByContactId(contactId);
        }
        
        public SalesOrder CreateObject(SalesOrder salesOrder, IContactService _contactService)
        {
            salesOrder.Errors = new HashSet<string>();
            return (_validator.ValidCreateObject(salesOrder, _contactService) ? _s.CreateObject(salesOrder) : salesOrder);
        }

        public SalesOrder CreateObject(int contactId, DateTime salesDate, IContactService _contactService)
        {
            SalesOrder so = new SalesOrder
            {
                CustomerId = contactId,
                SalesDate = salesDate
            };
            return this.CreateObject(so, _contactService);
        }

        public SalesOrder UpdateObject(SalesOrder salesOrder, IContactService _contactService)
        {
            return (_validator.ValidUpdateObject(salesOrder, _contactService) ? _s.UpdateObject(salesOrder) : salesOrder);
        }

        public SalesOrder SoftDeleteObject(SalesOrder salesOrder, ISalesOrderDetailService _salesOrderDetailService)
        {
            return (_validator.ValidDeleteObject(salesOrder, _salesOrderDetailService) ? _s.SoftDeleteObject(salesOrder) : salesOrder);
        }

        public bool DeleteObject(int Id)
        {
            return _s.DeleteObject(Id);
        }

        public SalesOrder ConfirmObject(SalesOrder salesOrder, ISalesOrderDetailService _sods,
                                        IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidConfirmObject(salesOrder, _sods))
            {
                _s.ConfirmObject(salesOrder);
                IList<SalesOrderDetail> details = _sods.GetObjectsBySalesOrderId(salesOrder.Id);
                foreach (var detail in details)
                {
                    _sods.ConfirmObject(detail, _stockMutationService, _itemService);
                }
            }
            return salesOrder;
        }

        public SalesOrder UnconfirmObject(SalesOrder salesOrder, ISalesOrderDetailService _salesOrderDetailService,
                                    IDeliveryOrderDetailService _deliveryOrderDetailService, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidUnconfirmObject(salesOrder, _salesOrderDetailService, _deliveryOrderDetailService, _itemService))
            {
                _s.UnconfirmObject(salesOrder);
                IList<SalesOrderDetail> details = _salesOrderDetailService.GetObjectsBySalesOrderId(salesOrder.Id);
                foreach (var detail in details)
                {
                    _salesOrderDetailService.UnconfirmObject(detail, _deliveryOrderDetailService, _stockMutationService, _itemService);
                }
            }
            return salesOrder;
        }
    }
}