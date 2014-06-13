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
    public class SalesInvoiceValidator : ISalesInvoiceValidator
    {
        public SalesInvoice VContact(SalesInvoice si, IContactService _cs)
        {
            Contact c = _cs.GetObjectById(si.ContactId);
            if (c == null)
            {
                si.Errors.Add("Contact", "Tidak boleh tidak ada");
            }
            return si;
        }

        public SalesInvoice VHasSalesInvoiceDetails(SalesInvoice si, ISalesInvoiceDetailService _sids)
        {
            IList<SalesInvoiceDetail> details = _sids.GetObjectsBySalesInvoiceId(si.Id);
            if (!details.Any())
            {
                si.Errors.Add("SalesInvoice", "Tidak boleh memilik Purchase Invoice Details");
            }
            return si;
        }

        public SalesInvoice VHasReceipt(SalesInvoice si, IReceivableService _receivableService, IReceiptVoucherDetailService _receiptVoucherDetailService)
        {
            Receivable receivable = _receivableService.GetObjectBySource("SalesInvoice", si.Id);
            IList<ReceiptVoucherDetail> pvdetails = _receiptVoucherDetailService.GetObjectsByReceivableId(receivable.Id);
            if (pvdetails.Any())
            {
                si.Errors.Add("ReceiptVoucherDetail", "Tidak boleh sudah ada proses pembayaran");
            }
            return si;
        }

        public SalesInvoice VIsConfirmed(SalesInvoice si)
        {
            if (si.IsConfirmed)
            {
                si.Errors.Add("SalesInvoice", "Tidak boleh sudah dikonfirmasi");
            }
            return si;
        }

        public SalesInvoice VCreateObject(SalesInvoice si, IContactService _cs)
        {
            VContact(si, _cs);
            return si;
        }

        public SalesInvoice VUpdateObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IContactService _cs)
        {
            VContact(si, _cs);
            VHasSalesInvoiceDetails(si, _sids);
            VIsConfirmed(si);
            return si;
        }

        public SalesInvoice VDeleteObject(SalesInvoice si)
        {
            VIsConfirmed(si);
            return si;
        }

        public SalesInvoice VConfirmObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods)
        {
            VIsConfirmed(si);
            VHasSalesInvoiceDetails(si, _sids);
            if (isValid(si))
            {
                IList<SalesInvoiceDetail> details = _sids.GetObjectsBySalesInvoiceId(si.Id);
                ISalesInvoiceDetailValidator detailvalidator = new SalesInvoiceDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VConfirmObject(detail, _sids, _dods);
                    foreach (var error in detail.Errors)
                    {
                        si.Errors.Add(error.Key, error.Value);
                    }
                    if (si.Errors.Any()) { return si; }
                }
            }
            return si;
        }

        public SalesInvoice VUnconfirmObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IReceiptVoucherDetailService _rvds, IReceivableService _receivableService)
        {
            VHasReceipt(si, _receivableService, _rvds);
            if (isValid(si))
            {
                IList<SalesInvoiceDetail> details = _sids.GetObjectsBySalesInvoiceId(si.Id);
                foreach (var detail in details)
                {
                    if (!_sids.GetValidator().ValidUnconfirmObject(detail, _rvds, _receivableService))
                    {
                        foreach (var error in detail.Errors)
                        {
                            si.Errors.Add(error.Key, error.Value);
                        }
                        if (si.Errors.Any()) { return si; }
                    }
                }
            }

            return si;
        }

        public bool ValidCreateObject(SalesInvoice si, IContactService _cs)
        {
            VCreateObject(si, _cs);
            return isValid(si);
        }

        public bool ValidUpdateObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IContactService _cs)
        {
            si.Errors.Clear();
            VUpdateObject(si, _sids, _cs);
            return isValid(si);
        }

        public bool ValidDeleteObject(SalesInvoice si)
        {
            si.Errors.Clear();
            VDeleteObject(si);
            return isValid(si);
        }

        public bool ValidConfirmObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods)
        {
            si.Errors.Clear();
            VConfirmObject(si, _sids, _dods);
            return isValid(si);
        }

        public bool ValidUnconfirmObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IReceiptVoucherDetailService _rvds, IReceivableService _receivableService)
        {
            si.Errors.Clear();
            VUnconfirmObject(si, _sids, _rvds, _receivableService);
            return isValid(si);
        }

        public bool isValid(SalesInvoice obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(SalesInvoice obj)
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