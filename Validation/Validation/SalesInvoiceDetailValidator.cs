using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;
using Core.Constant;

namespace Validation.Validation
{
    public class SalesInvoiceDetailValidator : ISalesInvoiceDetailValidator
    {

        public SalesInvoiceDetail VHasDeliveryOrderDetail(SalesInvoiceDetail sid, IDeliveryOrderDetailService _dods)
        {
            DeliveryOrderDetail dod = _dods.GetObjectById(sid.DeliveryOrderDetailId);
            if (dod == null)
            {
                sid.Errors.Add("DeliveryOrderDetail", "Tidak boleh tidak ada");
            }
            return sid;
        }

        public SalesInvoiceDetail VQuantity(SalesInvoiceDetail sid, IDeliveryOrderDetailService _dods)
        {
            DeliveryOrderDetail dod = _dods.GetObjectById(sid.DeliveryOrderDetailId);
            if (sid.Quantity > dod.Quantity)
            {
                sid.Errors.Add("Quantity", "Tidak boleh lebih besar dari Delivery Order");
            }
            return sid;
        }

        public SalesInvoiceDetail VPrice(SalesInvoiceDetail sid)
        {
            if (sid.Amount < 0)
            {
                sid.Errors.Add("Price", "Tidak boleh negatif");
            }
            return sid;
        }

        public SalesInvoiceDetail VIsUniqueDeliveryOrderDetail(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sid, IDeliveryOrderDetailService _dods)
        {
            IList<SalesInvoiceDetail> details = _sid.GetObjectsBySalesInvoiceId(sid.SalesInvoiceId);
            foreach (var detail in details)
            {
                if (detail.DeliveryOrderDetailId == sid.DeliveryOrderDetailId && detail.Id != sid.Id)
                {
                    sid.Errors.Add("SalesInvoiceDetail", "Tidak boleh memiliki lebih dari 2 Delivery Order Detail");
                }
            }
            return sid;

        }

        public SalesInvoiceDetail VHasReceipt(SalesInvoiceDetail sid, IReceiptVoucherDetailService _rvds, IReceivableService _receivableService)
        {
            //TODO:
            return sid;
        }

        public SalesInvoiceDetail VIsConfirmed(SalesInvoiceDetail sid)
        {
            if (sid.IsConfirmed)
            {
                sid.Errors.Add("IsConfirmed", "Tidak boleh sudah dikonfirmasi");
            }
            return sid;
        }

        public SalesInvoiceDetail VCreateObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sid, IDeliveryOrderDetailService _dods)
        {
            VHasDeliveryOrderDetail(sid, _dods);
            VQuantity(sid, _dods);
            VPrice(sid);
            VIsUniqueDeliveryOrderDetail(sid, _sid, _dods);
            return sid;
        }

        public SalesInvoiceDetail VUpdateObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods)
        {
            VIsConfirmed(sid);
            VHasDeliveryOrderDetail(sid, _dods);
            VQuantity(sid, _dods);
            VPrice(sid);
            VIsUniqueDeliveryOrderDetail(sid, _sids, _dods);
            return sid;
        }

        public SalesInvoiceDetail VDeleteObject(SalesInvoiceDetail sid)
        {
            VIsConfirmed(sid);
            return sid;
        }

        public SalesInvoiceDetail VConfirmObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods)
        {
            VQuantity(sid, _dods);
            VPrice(sid);
            VIsUniqueDeliveryOrderDetail(sid, _sids, _dods);
            return sid;
        }

        public SalesInvoiceDetail VUnconfirmObject(SalesInvoiceDetail sid,  IReceiptVoucherDetailService _rvds, IReceivableService _receivableService)
        {
            VHasReceipt(sid, _rvds, _receivableService);
            return sid;
        }

        public bool ValidCreateObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods)
        {
            VCreateObject(sid, _sids, _dods);
            return isValid(sid);
        }

        public bool ValidUpdateObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods)
        {
            sid.Errors.Clear();
            VUpdateObject(sid, _sids, _dods);
            return isValid(sid);
        }

        public bool ValidDeleteObject(SalesInvoiceDetail sid)
        {
            sid.Errors.Clear();
            VDeleteObject(sid);
            return isValid(sid);
        }

        public bool ValidConfirmObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods)
        {
            sid.Errors.Clear();
            VConfirmObject(sid, _sids, _dods);
            return isValid(sid);
        }

        public bool ValidUnconfirmObject(SalesInvoiceDetail sid, IReceiptVoucherDetailService _rvds, IReceivableService _receivableService)
        {
            sid.Errors.Clear();
            VUnconfirmObject(sid, _rvds, _receivableService);
            return isValid(sid);
        }

        public bool isValid(SalesInvoiceDetail obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(SalesInvoiceDetail obj)
        {
            string erroroutput = "";
            KeyValuePair<string, string> first = obj.Errors.ElementAt(0);
            erroroutput += first.Key + "," + first.Value;
            foreach (KeyValuePair<string, string> pair in obj.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += pair.Key + "," + pair.Value;
            }
            return erroroutput;
        }

    }
}