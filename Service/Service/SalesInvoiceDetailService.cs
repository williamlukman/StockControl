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
    public class SalesInvoiceDetailService : ISalesInvoiceDetailService
    {
        private ISalesInvoiceDetailRepository _repository;
        private ISalesInvoiceDetailValidator _validator;

        public SalesInvoiceDetailService(ISalesInvoiceDetailRepository _salesInvoiceDetailRepository, ISalesInvoiceDetailValidator _salesInvoiceDetailValidator)
        {
            _repository = _salesInvoiceDetailRepository;
            _validator = _salesInvoiceDetailValidator;
        }

        public ISalesInvoiceDetailValidator GetValidator()
        {
            return _validator;
        }

        public IList<SalesInvoiceDetail> GetObjectsBySalesInvoiceId(int salesInvoiceId)
        {
            return _repository.GetObjectsBySalesInvoiceId(salesInvoiceId);
        }

        public SalesInvoiceDetail GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public SalesInvoiceDetail CreateObject(SalesInvoiceDetail salesInvoiceDetail, ISalesInvoiceService _sis, IDeliveryOrderDetailService _dods)
        {
            salesInvoiceDetail.Errors = new Dictionary<String, String>();
            if (_validator.ValidCreateObject(salesInvoiceDetail, this, _dods))
            {
                salesInvoiceDetail.ContactId = _sis.GetObjectById(salesInvoiceDetail.SalesInvoiceId).ContactId;
                return _repository.CreateObject(salesInvoiceDetail);
            }
            else
            {
                return salesInvoiceDetail;
            }
        }

        public SalesInvoiceDetail CreateObject(int salesInvoiceId, int purchaseReceivalDetailId, int quantity, decimal amount, ISalesInvoiceService _sis, IDeliveryOrderDetailService _dods)
        {
            SalesInvoiceDetail sid = new SalesInvoiceDetail
            {
                SalesInvoiceId = salesInvoiceId,
                DeliveryOrderDetailId = purchaseReceivalDetailId,
                ContactId = 0,
                Quantity = quantity,
                Amount = amount
            };
            return this.CreateObject(sid, _sis, _dods);
        }

        public SalesInvoiceDetail UpdateObject(SalesInvoiceDetail salesInvoiceDetail, IDeliveryOrderDetailService _dods)
        {
            return (_validator.ValidUpdateObject(salesInvoiceDetail, this, _dods) ?
                     _repository.UpdateObject(salesInvoiceDetail) : salesInvoiceDetail);
        }

        public SalesInvoiceDetail SoftDeleteObject(SalesInvoiceDetail salesInvoiceDetail)
        {
            return (_validator.ValidDeleteObject(salesInvoiceDetail) ? _repository.SoftDeleteObject(salesInvoiceDetail) : salesInvoiceDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public SalesInvoiceDetail ConfirmObject(SalesInvoiceDetail salesInvoiceDetail, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods)
        {
            if (_validator.ValidConfirmObject(salesInvoiceDetail, _sids, _dods))
            {
                salesInvoiceDetail = _repository.ConfirmObject(salesInvoiceDetail);
            }
            return salesInvoiceDetail;
        }

        public SalesInvoiceDetail UnconfirmObject(SalesInvoiceDetail salesInvoiceDetail, IReceiptVoucherDetailService _rvds, IReceivableService _receivableService)
        {
            if (_validator.ValidUnconfirmObject(salesInvoiceDetail, _rvds, _receivableService))
            {
                salesInvoiceDetail = _repository.UnconfirmObject(salesInvoiceDetail);
            }
            return salesInvoiceDetail;
        }
    }
}