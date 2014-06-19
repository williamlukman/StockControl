using Core.DomainModel;
using Core.Interface.Repository;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PaymentVoucherRepository : EfRepository<PaymentVoucher>, IPaymentVoucherRepository
    {
        private StockControlEntities stocks;
        public PaymentVoucherRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<PaymentVoucher> GetAll()
        {
            return FindAll(pv => !pv.IsDeleted).ToList();
        }

        public IList<PaymentVoucher> GetObjectsByCashBankId(int cashBankId)
        {
            return FindAll(pv => pv.CashBankId == cashBankId && !pv.IsDeleted).ToList();
        }

        public PaymentVoucher GetObjectById(int Id)
        {
            PaymentVoucher paymentVoucher = Find(pv => pv.Id == Id && !pv.IsDeleted);
            if (paymentVoucher != null) { paymentVoucher.Errors = new Dictionary<string, string>(); }
            return paymentVoucher;
        }

        public IList<PaymentVoucher> GetObjectsByContactId(int contactId)
        {
            return FindAll(pv => pv.ContactId == contactId && !pv.IsDeleted).ToList();
        }

        public PaymentVoucher CreateObject(PaymentVoucher paymentVoucher)
        {
            paymentVoucher.PendingClearanceAmount = 0;
            paymentVoucher.Code = SetObjectCode();
            paymentVoucher.IsDeleted = false;
            paymentVoucher.IsConfirmed = false;
            paymentVoucher.CreatedAt = DateTime.Now;
            return Create(paymentVoucher);
        }

        public PaymentVoucher UpdateObject(PaymentVoucher paymentVoucher)
        {
            paymentVoucher.ModifiedAt = DateTime.Now;
            Update(paymentVoucher);
            return paymentVoucher;
        }

        public PaymentVoucher SoftDeleteObject(PaymentVoucher paymentVoucher)
        {
            paymentVoucher.IsDeleted = true;
            paymentVoucher.DeletedAt = DateTime.Now;
            Update(paymentVoucher);
            return paymentVoucher;
        }

        public bool DeleteObject(int Id)
        {
            PaymentVoucher pv = Find(x => x.Id == Id);
            return (Delete(pv) == 1) ? true : false;
        }

        public PaymentVoucher ConfirmObject(PaymentVoucher paymentVoucher)
        {
            paymentVoucher.IsConfirmed = true;
            Update(paymentVoucher);
            return paymentVoucher;
        }

        public PaymentVoucher UnconfirmObject(PaymentVoucher paymentVoucher)
        {
            paymentVoucher.IsConfirmed = false;
            Update(paymentVoucher);
            return paymentVoucher;
        }

        public PaymentVoucher ClearObject(PaymentVoucher paymentVoucher)
        {
            paymentVoucher.IsCleared = true;
            Update(paymentVoucher);
            return paymentVoucher;
        }

        public PaymentVoucher UnclearObject(PaymentVoucher paymentVoucher)
        {
            paymentVoucher.IsCleared = false;
            Update(paymentVoucher);
            return paymentVoucher;
        }

        public string SetObjectCode()
        {
            // Code: #{year}/#{total_number
            int totalobject = FindAll().Count() + 1;
            string Code = "#" + DateTime.Now.Year.ToString() + "/#" + totalobject;
            return Code;
        }
    }
}