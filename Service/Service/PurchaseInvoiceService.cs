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
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private IPurchaseInvoiceRepository _repository;
        private IPurchaseInvoiceValidator _validator;

        public PurchaseInvoiceService(IPurchaseInvoiceRepository _purchaseInvoiceRepository, IPurchaseInvoiceValidator _purchaseInvoiceValidator)
        {
            _repository = _purchaseInvoiceRepository;
            _validator = _purchaseInvoiceValidator;
        }

        public IPurchaseInvoiceValidator GetValidator()
        {
            return _validator;
        }

        public IList<PurchaseInvoice> GetAll()
        {
            return _repository.GetAll();
        }

        public PurchaseInvoice GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public IList<PurchaseInvoice> GetObjectsByContactId(int contactId)
        {
            return _repository.GetObjectsByContactId(contactId);
        }

        public PurchaseInvoice CreateObject(PurchaseInvoice purchaseInvoice, IContactService _contactService)
        {
            purchaseInvoice.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(purchaseInvoice, _contactService) ? _repository.CreateObject(purchaseInvoice) : purchaseInvoice);
        }

        public PurchaseInvoice CreateObject(int contactId, string description, decimal totalAmount, IContactService _contactService)
        {
            PurchaseInvoice pi = new PurchaseInvoice
            {
                ContactId = contactId,
                Description = description,
                TotalAmount = totalAmount,
            };
            return this.CreateObject(pi, _contactService);
        }

        public PurchaseInvoice UpdateObject(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService, IContactService _contactService)
        {
            return (_validator.ValidUpdateObject(purchaseInvoice, this, _purchaseInvoiceDetailService, _contactService) ? _repository.UpdateObject(purchaseInvoice) : purchaseInvoice);
        }

        public PurchaseInvoice SoftDeleteObject(PurchaseInvoice purchaseInvoice)
        {
            return (_validator.ValidDeleteObject(purchaseInvoice) ? _repository.SoftDeleteObject(purchaseInvoice) : purchaseInvoice);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public PurchaseInvoice ConfirmObject(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService, IPurchaseReceivalDetailService _prds, IPayableService _payableService)
        {
            if (_validator.ValidConfirmObject(purchaseInvoice, _purchaseInvoiceDetailService, _prds))
            {
                _repository.ConfirmObject(purchaseInvoice);
                IList<PurchaseInvoiceDetail> details = _purchaseInvoiceDetailService.GetObjectsByPurchaseInvoiceId(purchaseInvoice.Id);
                foreach (var detail in details)
                {
                    detail.ConfirmedAt = purchaseInvoice.ConfirmedAt;
                    _purchaseInvoiceDetailService.ConfirmObject(detail, _purchaseInvoiceDetailService, _prds);
                }
                _payableService.CreateObject(purchaseInvoice.ContactId, "PurchaseInvoice", purchaseInvoice.Id, purchaseInvoice.TotalAmount);
            }
            return purchaseInvoice;
        }

        public PurchaseInvoice UnconfirmObject(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService,
                                               IPaymentVoucherDetailService _pvds, IPayableService _payableService)
        {
            if (_validator.ValidUnconfirmObject(purchaseInvoice, _purchaseInvoiceDetailService, _pvds, _payableService))
            {
                _repository.UnconfirmObject(purchaseInvoice);
                IList<PurchaseInvoiceDetail> details = _purchaseInvoiceDetailService.GetObjectsByPurchaseInvoiceId(purchaseInvoice.Id);
                foreach (var detail in details)
                {
                    _purchaseInvoiceDetailService.UnconfirmObject(detail, _pvds, _payableService);
                }
                Payable payable = _payableService.GetObjectBySource("PurchaseInvoice", purchaseInvoice.Id);
                _payableService.SoftDeleteObject(payable);
            }
            return purchaseInvoice;
        }
    }
}