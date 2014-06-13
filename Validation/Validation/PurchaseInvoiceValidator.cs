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
    public class PurchaseInvoiceValidator : IPurchaseInvoiceValidator
    {
        public PurchaseInvoice VContact(PurchaseInvoice pi, IContactService _cs)
        {
            Contact c = _cs.GetObjectById(pi.ContactId);
            if (c == null)
            {
                pi.Errors.Add("Contact", "Tidak boleh tidak ada");
            }
            return pi;
        }

        public PurchaseInvoice VHasPurchaseInvoiceDetails(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids)
        {
            IList<PurchaseInvoiceDetail> details = _pids.GetObjectsByPurchaseInvoiceId(pi.Id);
            if (!details.Any())
            {
                pi.Errors.Add("PurchaseInvoice", "Tidak boleh memilik Purchase Invoice Details");
            }
            return pi;
        }

        public PurchaseInvoice VHasPayment(PurchaseInvoice pi, IPayableService _payableService, IPaymentVoucherDetailService _paymentVoucherDetailService)
        {
            Payable payable = _payableService.GetObjectBySource("PurchaseInvoice", pi.Id);
            IList<PaymentVoucherDetail> pvdetails = _paymentVoucherDetailService.GetObjectsByPayableId(payable.Id);
            if (pvdetails.Any())
            {
                pi.Errors.Add("PaymentVoucherDetail", "Tidak boleh sudah ada proses pembayaran");
            }
            return pi;
        }

        public PurchaseInvoice VIsConfirmed(PurchaseInvoice pi)
        {
            if (pi.IsConfirmed)
            {
                pi.Errors.Add("PurchaseInvoice", "Tidak boleh sudah dikonfirmasi");
            }
            return pi;
        }

        public PurchaseInvoice VCreateObject(PurchaseInvoice pi, IContactService _cs)
        {
            VContact(pi, _cs);
            return pi;
        }

        public PurchaseInvoice VUpdateObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IContactService _cs)
        {
            VContact(pi, _cs);
            VHasPurchaseInvoiceDetails(pi, _pids);
            VIsConfirmed(pi);
            return pi;
        }

        public PurchaseInvoice VDeleteObject(PurchaseInvoice pi)
        {
            VIsConfirmed(pi);
            return pi;
        }

        public PurchaseInvoice VConfirmObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds)
        {
            VIsConfirmed(pi);
            VHasPurchaseInvoiceDetails(pi, _pids);
            if (isValid(pi))
            {
                IList<PurchaseInvoiceDetail> details = _pids.GetObjectsByPurchaseInvoiceId(pi.Id);
                IPurchaseInvoiceDetailValidator detailvalidator = new PurchaseInvoiceDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VConfirmObject(detail, _pids, _prds);
                    foreach (var error in detail.Errors)
                    {
                        pi.Errors.Add(error.Key, error.Value);
                    }
                    if (pi.Errors.Any()) { return pi; }
                }
            }
            return pi;
        }

        public PurchaseInvoice VUnconfirmObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IPaymentVoucherDetailService _pvds, IPayableService _payableService)
        {
            VHasPayment(pi, _payableService, _pvds);
            if (isValid(pi))
            {
                IList<PurchaseInvoiceDetail> details = _pids.GetObjectsByPurchaseInvoiceId(pi.Id);
                foreach (var detail in details)
                {
                    if (!_pids.GetValidator().ValidUnconfirmObject(detail, _pvds, _payableService))
                    {
                        foreach (var error in detail.Errors)
                        {
                            pi.Errors.Add(error.Key, error.Value);
                        }
                        if (pi.Errors.Any()) { return pi; }
                    }
                }
            }

            return pi;
        }

        public bool ValidCreateObject(PurchaseInvoice pi, IContactService _cs)
        {
            VCreateObject(pi, _cs);
            return isValid(pi);
        }

        public bool ValidUpdateObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IContactService _cs)
        {
            pi.Errors.Clear();
            VUpdateObject(pi, _pids, _cs);
            return isValid(pi);
        }

        public bool ValidDeleteObject(PurchaseInvoice pi)
        {
            pi.Errors.Clear();
            VDeleteObject(pi);
            return isValid(pi);
        }

        public bool ValidConfirmObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds)
        {
            pi.Errors.Clear();
            VConfirmObject(pi, _pids, _prds);
            return isValid(pi);
        }

        public bool ValidUnconfirmObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IPaymentVoucherDetailService _pvds, IPayableService _payableService)
        {
            pi.Errors.Clear();
            VUnconfirmObject(pi, _pids, _pvds, _payableService);
            return isValid(pi);
        }

        public bool isValid(PurchaseInvoice obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(PurchaseInvoice obj)
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