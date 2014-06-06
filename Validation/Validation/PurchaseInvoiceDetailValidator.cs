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
    public class PurchaseInvoiceDetailValidator : IPurchaseInvoiceDetailValidator
    {

        public PurchaseInvoiceDetail VHasPurchaseReceivalDetail(PurchaseInvoiceDetail pid, IPurchaseReceivalDetailService _prds)
        {
            PurchaseReceivalDetail prd = _prds.GetObjectById(pid.PurchaseReceivalDetailId);
            if (prd == null)
            {
                pid.Errors.Add("PurchaseReceivalDetail", "Tidak boleh tidak ada");
            }
            return pid;
        }

        public PurchaseInvoiceDetail VQuantity(PurchaseInvoiceDetail pid, IPurchaseReceivalDetailService _prds)
        {
            PurchaseReceivalDetail prd = _prds.GetObjectById(pid.PurchaseReceivalDetailId);
            if (pid.Quantity > prd.Quantity)
            {
                pid.Errors.Add("Quantity", "Tidak boleh lebih besar dari Purchase Receival");
            }
            return pid;
        }

        public PurchaseInvoiceDetail VPrice(PurchaseInvoiceDetail pid)
        {
            if (pid.Amount < 0)
            {
                pid.Errors.Add("Price", "Tidak boleh negatif");
            }
            return pid;
        }

        public PurchaseInvoiceDetail VIsUniquePurchaseReceivalDetail(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pid, IPurchaseReceivalDetailService _prds)
        {
            IList<PurchaseInvoiceDetail> details = _pid.GetObjectsByPurchaseInvoiceId(pid.PurchaseInvoiceId);
            foreach (var detail in details)
            {
                if (detail.PurchaseReceivalDetailId == pid.PurchaseReceivalDetailId && detail.Id != pid.Id)
                {
                    pid.Errors.Add("PurchaseInvoiceDetail", "Tidak boleh memiliki lebih dari 2 Purchase Receival Detail");
                }
            }
            return pid;

        }

        public PurchaseInvoiceDetail VHasPayment(PurchaseInvoiceDetail pid, IPaymentVoucherDetailService _pvds, IPayableService _payableService)
        {
            //TODO:
            return pid;
        }

        public PurchaseInvoiceDetail VIsConfirmed(PurchaseInvoiceDetail pid)
        {
            if (pid.IsConfirmed)
            {
                pid.Errors.Add("IsConfirmed", "Tidak boleh sudah dikonfirmasi");
            }
            return pid;
        }

        public PurchaseInvoiceDetail VCreateObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pid, IPurchaseReceivalDetailService _prds)
        {
            VHasPurchaseReceivalDetail(pid, _prds);
            VQuantity(pid, _prds);
            VPrice(pid);
            VIsUniquePurchaseReceivalDetail(pid, _pid, _prds);
            return pid;
        }

        public PurchaseInvoiceDetail VUpdateObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds)
        {
            VIsConfirmed(pid);
            VHasPurchaseReceivalDetail(pid, _prds);
            VQuantity(pid, _prds);
            VPrice(pid);
            VIsUniquePurchaseReceivalDetail(pid, _pids, _prds);
            return pid;
        }

        public PurchaseInvoiceDetail VDeleteObject(PurchaseInvoiceDetail pid)
        {
            VIsConfirmed(pid);
            return pid;
        }

        public PurchaseInvoiceDetail VConfirmObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds)
        {
            VQuantity(pid, _prds);
            VPrice(pid);
            VIsUniquePurchaseReceivalDetail(pid, _pids, _prds);
            return pid;
        }

        public PurchaseInvoiceDetail VUnconfirmObject(PurchaseInvoiceDetail pid,  IPaymentVoucherDetailService _pvds, IPayableService _payableService)
        {
            VHasPayment(pid, _pvds, _payableService);
            return pid;
        }

        public bool ValidCreateObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds)
        {
            VCreateObject(pid, _pids, _prds);
            return isValid(pid);
        }

        public bool ValidUpdateObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds)
        {
            pid.Errors.Clear();
            VUpdateObject(pid, _pids, _prds);
            return isValid(pid);
        }

        public bool ValidDeleteObject(PurchaseInvoiceDetail pid)
        {
            pid.Errors.Clear();
            VDeleteObject(pid);
            return isValid(pid);
        }

        public bool ValidConfirmObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds)
        {
            pid.Errors.Clear();
            VConfirmObject(pid, _pids, _prds);
            return isValid(pid);
        }

        public bool ValidUnconfirmObject(PurchaseInvoiceDetail pid, IPaymentVoucherDetailService _pvds, IPayableService _payableService)
        {
            pid.Errors.Clear();
            VUnconfirmObject(pid, _pvds, _payableService);
            return isValid(pid);
        }

        public bool isValid(PurchaseInvoiceDetail obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(PurchaseInvoiceDetail obj)
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