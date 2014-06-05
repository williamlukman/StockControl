using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PurchaseInvoiceDetailService : IPurchaseInvoiceDetailService
    {
        private IPurchaseInvoiceDetailRepository _repository;
        private IPurchaseInvoiceDetailValidator _validator;

        public PurchaseInvoiceDetailService(IPurchaseInvoiceDetailRepository _purchaseInvoiceDetailRepository, IPurchaseInvoiceDetailValidator _purchaseInvoiceDetailValidator)
        {
            _repository = _purchaseInvoiceDetailRepository;
            _validator = _purchaseInvoiceDetailValidator;
        }

        public IPurchaseInvoiceDetailValidator GetValidator()
        {
            return _validator;
        }

        public IList<PurchaseInvoiceDetail> GetObjectsByPurchaseInvoiceId(int purchaseInvoiceId)
        {
            return _repository.GetObjectsByPurchaseInvoiceId(purchaseInvoiceId);
        }

        public PurchaseInvoiceDetail GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public PurchaseInvoiceDetail CreateObject(PurchaseInvoiceDetail purchaseInvoiceDetail, IPurchaseInvoiceService _pis, IPurchaseReceivalDetailService _prds)
        {
            purchaseInvoiceDetail.Errors = new Dictionary<String, String>();
            if (_validator.ValidCreateObject(purchaseInvoiceDetail, this, _prds))
            {
                purchaseInvoiceDetail.ContactId = _pis.GetObjectById(purchaseInvoiceDetail.PurchaseInvoiceId).ContactId;
                return _repository.CreateObject(purchaseInvoiceDetail);
            }
            else
            {
                return purchaseInvoiceDetail;
            }
        }

        public PurchaseInvoiceDetail CreateObject(int purchaseInvoiceId, int purchaseReceivalDetailId, int itemId, int quantity, decimal price, IPurchaseInvoiceService _pis, IPurchaseReceivalDetailService _prds)
        {
            PurchaseInvoiceDetail pid = new PurchaseInvoiceDetail
            {
                PurchaseInvoiceId = purchaseInvoiceId,
                PurchaseReceivalDetailId = purchaseReceivalDetailId,
                ContactId = 0,
                Quantity = quantity,
                Price = price
            };
            return this.CreateObject(pid, _pis, _prds);
        }

        public PurchaseInvoiceDetail UpdateObject(PurchaseInvoiceDetail purchaseInvoiceDetail, IPurchaseReceivalDetailService _prds)
        {
            return (_validator.ValidUpdateObject(purchaseInvoiceDetail, this, _prds) ?
                     _repository.UpdateObject(purchaseInvoiceDetail) : purchaseInvoiceDetail);
        }

        public PurchaseInvoiceDetail SoftDeleteObject(PurchaseInvoiceDetail purchaseInvoiceDetail)
        {
            return (_validator.ValidDeleteObject(purchaseInvoiceDetail) ? _repository.SoftDeleteObject(purchaseInvoiceDetail) : purchaseInvoiceDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public PurchaseInvoiceDetail ConfirmObject(PurchaseInvoiceDetail purchaseInvoiceDetail, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds)
        {
            if (_validator.ValidConfirmObject(purchaseInvoiceDetail, _pids, _prds))
            {
                purchaseInvoiceDetail = _repository.ConfirmObject(purchaseInvoiceDetail);
            }
            return purchaseInvoiceDetail;
        }

        public PurchaseInvoiceDetail UnconfirmObject(PurchaseInvoiceDetail purchaseInvoiceDetail, IPaymentVoucherDetailService _pvds, IPayableService _payableService)
        {
            if (_validator.ValidUnconfirmObject(purchaseInvoiceDetail, _pvds, _payableService))
            {
                purchaseInvoiceDetail = _repository.UnconfirmObject(purchaseInvoiceDetail);
            }
            return purchaseInvoiceDetail;
        }
    }
}