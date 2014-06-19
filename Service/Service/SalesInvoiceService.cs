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
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private ISalesInvoiceRepository _repository;
        private ISalesInvoiceValidator _validator;

        public SalesInvoiceService(ISalesInvoiceRepository _salesInvoiceRepository, ISalesInvoiceValidator _salesInvoiceValidator)
        {
            _repository = _salesInvoiceRepository;
            _validator = _salesInvoiceValidator;
        }

        public ISalesInvoiceValidator GetValidator()
        {
            return _validator;
        }

        public IList<SalesInvoice> GetAll()
        {
            return _repository.GetAll();
        }

        public SalesInvoice GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public IList<SalesInvoice> GetObjectsByContactId(int contactId)
        {
            return _repository.GetObjectsByContactId(contactId);
        }

        public SalesInvoice CreateObject(SalesInvoice salesInvoice, IContactService _contactService)
        {
            salesInvoice.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(salesInvoice, _contactService) ? _repository.CreateObject(salesInvoice) : salesInvoice);
        }

        public SalesInvoice CreateObject(int contactId, string description, decimal totalAmount, IContactService _contactService)
        {
            SalesInvoice pi = new SalesInvoice
            {
                ContactId = contactId,
                Description = description,
                TotalAmount = totalAmount,
            };
            return this.CreateObject(pi, _contactService);
        }

        public SalesInvoice UpdateObject(SalesInvoice salesInvoice, ISalesInvoiceDetailService _salesInvoiceDetailService, IContactService _contactService)
        {
            return (_validator.ValidUpdateObject(salesInvoice, _salesInvoiceDetailService, _contactService) ? _repository.UpdateObject(salesInvoice) : salesInvoice);
        }

        public SalesInvoice SoftDeleteObject(SalesInvoice salesInvoice)
        {
            return (_validator.ValidDeleteObject(salesInvoice) ? _repository.SoftDeleteObject(salesInvoice) : salesInvoice);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public SalesInvoice ConfirmObject(SalesInvoice salesInvoice, ISalesInvoiceDetailService _salesInvoiceDetailService, IDeliveryOrderDetailService _deliveryOrderDetailService, IReceivableService _receivableService)
        {
            if (_validator.ValidConfirmObject(salesInvoice, _salesInvoiceDetailService, _deliveryOrderDetailService))
            {
                _repository.ConfirmObject(salesInvoice);
                IList<SalesInvoiceDetail> details = _salesInvoiceDetailService.GetObjectsBySalesInvoiceId(salesInvoice.Id);
                foreach (var detail in details)
                {
                    detail.ConfirmedAt = salesInvoice.ConfirmedAt;
                    _salesInvoiceDetailService.ConfirmObject(detail, _salesInvoiceDetailService, _deliveryOrderDetailService);
                }
                _receivableService.CreateObject(salesInvoice.ContactId, "SalesInvoice", salesInvoice.Id, salesInvoice.TotalAmount);
            }
            return salesInvoice;
        }

        public SalesInvoice UnconfirmObject(SalesInvoice salesInvoice, ISalesInvoiceDetailService _salesInvoiceDetailService,
                                               IReceiptVoucherDetailService _rvds, IReceivableService _receivableService)
        {
            if (_validator.ValidUnconfirmObject(salesInvoice, _salesInvoiceDetailService, _rvds, _receivableService))
            {
                _repository.UnconfirmObject(salesInvoice);
                IList<SalesInvoiceDetail> details = _salesInvoiceDetailService.GetObjectsBySalesInvoiceId(salesInvoice.Id);
                foreach (var detail in details)
                {
                    _salesInvoiceDetailService.UnconfirmObject(detail, _rvds, _receivableService);
                }
                Receivable receivable = _receivableService.GetObjectBySource("SalesInvoice", salesInvoice.Id);
                _receivableService.SoftDeleteObject(receivable);
            }
            return salesInvoice;
        }
    }
}