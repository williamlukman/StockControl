
using Core.DomainModel;
using Core.Interface.Service;
using Core.Interface.Validation;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Validation;

namespace ConsoleApp.Validation
{
    public class PVValidation
    {
        private IContactService _c;
        private IItemService _i;
        private IPurchaseOrderService _po;
        private IPurchaseOrderDetailService _pod;
        private IPurchaseReceivalService _pr;
        private IPurchaseReceivalDetailService _prd;
        private ISalesOrderService _so;
        private ISalesOrderDetailService _sod;
        private IDeliveryOrderService _do;
        private IDeliveryOrderDetailService _dod;
        private IStockMutationService _sm;
        private IPurchaseInvoiceService _pi;
        private IPurchaseInvoiceDetailService _pid;
        private IPayableService _payable;
        private IPaymentVoucherService _pv;
        private IPaymentVoucherDetailService _pvd;
        private ICashBankService _cb;

        public PVValidation(     IPurchaseInvoiceService pi, IPurchaseInvoiceDetailService pid,
                                 IPayableService payable, IPaymentVoucherService pv, IPaymentVoucherDetailService pvd,
                                 ICashBankService cb,
                                 IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            _c = c;
            _i = i;
            _sm = sm;
            _po = po;
            _pr = pr;
            _so = so;
            _do = d;
            _pod = pod;
            _prd = prd;
            _sod = sod;
            _dod = dod;
            _pi = pi;
            _pid = pid;
            _pv = pv;
            _pvd = pvd;
            _payable = payable;
            _cb = cb;
        }

        public int PayableValidation1(int piId)
        {
            Console.WriteLine("     [PY 1] Create valid Payable 10jt for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            Payable payable = _payable.CreateObject(c.Id, "PurchaseInvoice", piId, 10000000);
            if (payable.Errors.Any()) { Console.WriteLine("        >> " + _payable.GetValidator().PrintError(payable)); return 0; }
            return payable.Id;
        }

        public int PayableValidation2(int piId)
        {
            Console.WriteLine("     [PY 2] Create valid Payable 5jt for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            Payable payable = _payable.CreateObject(c.Id, "PurchaseInvoice", piId, 5000000);
            if (payable.Errors.Any()) { Console.WriteLine("        >> " + _payable.GetValidator().PrintError(payable)); return 0; }
            return payable.Id;
        }


        public int PVValidation1()
        {
            Console.WriteLine("     [PV 1] Create valid Payment Voucher by Bank for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            CashBank cb = _cb.GetObjectByName("Mandiri");
            PaymentVoucher pv = _pv.CreateObject(cb.Id, c.Id, DateTime.Today, 20000000, _pvd, _payable, _c, _cb);
            if (pv.Errors.Any()) { Console.WriteLine("        >> " + _pv.GetValidator().PrintError(pv)); return 0; }
            return pv.Id;
        }

        public int PVValidation2a(int pvId, int payableId)
        {
            Console.WriteLine("     [PV 2a] Create valid Payment Voucher Detail for Michaelangelo");
            PaymentVoucherDetail pvd = _pvd.CreateObject(pvId, payableId, (decimal) 3000000, "Payment 3jt untuk Payable 10jt", false, _pv, _cb, _payable, _c);
            if (pvd.Errors.Any()) { Console.WriteLine("        >> " + _pvd.GetValidator().PrintError(pvd)); return 0; }
            return pvd.Id;
        }

        public int PVValidation2b(int pvId, int payableId)
        {
            Console.WriteLine("     [PV 2b] Create valid Payment Voucher Detail for Michaelangelo");
            PaymentVoucherDetail pvd = _pvd.CreateObject(pvId, payableId, (decimal)2000000, "Payment 2jt untuk Payable 5jt", false, _pv, _cb, _payable, _c);
            if (pvd.Errors.Any()) { Console.WriteLine("        >> " + _pvd.GetValidator().PrintError(pvd)); return 0; }
            return pvd.Id;
        }

        public int PVValidation2c(int pvId, int payableId)
        {
            Console.WriteLine("     [PV 2c] Create valid Payment Voucher Detail for Michaelangelo");
            PaymentVoucherDetail pvd = _pvd.CreateObject(pvId, payableId, (decimal)4000000, "Payment 4jt untuk Payable 5jt", false, _pv, _cb, _payable, _c);
            if (pvd.Errors.Any()) { Console.WriteLine("        >> " + _pvd.GetValidator().PrintError(pvd)); return 0; }
            return pvd.Id;
        }

        public int PVValidation4()
        {
            Console.WriteLine("     [PV 4] Create valid Payment Voucher by Cash for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            CashBank cb = _cb.GetObjectByName("SSD PettyCash");
            PaymentVoucher pv = _pv.CreateObject(cb.Id, c.Id, DateTime.Today, 7500000, _pvd, _payable, _c, _cb);
            if (pv.Errors.Any()) { Console.WriteLine("        >> " + _pv.GetValidator().PrintError(pv)); return 0; }
            return pv.Id;
        }

        public int PVValidation5(int pvId, int payableId)
        {
            Console.WriteLine("     [PV 5] Create valid Payment Voucher Detail for Michaelangelo");
            PaymentVoucherDetail pvd = _pvd.CreateObject(pvId, payableId, (decimal)4000000, "Payment 4jt untuk Payable 10jt", true, _pv, _cb, _payable, _c);
            if (pvd.Errors.Any()) { Console.WriteLine("        >> " + _pvd.GetValidator().PrintError(pvd)); return 0; }
            return pvd.Id;
        }

        public int PVValidation6(int pvId, int payableId)
        {
            Console.WriteLine("     [PV 6] Create valid Payment Voucher Detail for Michaelangelo");
            PaymentVoucherDetail pvd = _pvd.CreateObject(pvId, payableId, (decimal)3500000, "Payment 3.5jt untuk Payable 5jt", true, _pv, _cb, _payable, _c);
            if (pvd.Errors.Any()) { Console.WriteLine("        >> " + _pvd.GetValidator().PrintError(pvd)); return 0; }
            return pvd.Id;
        }

        public void PVValidation7(int pvId)
        {
            Console.WriteLine("     [PV 7] Confirm PV1 Michaelangelo");
            PaymentVoucher pv = _pv.ConfirmObject(_pv.GetObjectById(pvId), _pvd, _cb, _payable, _c);
            if (pv.Errors.Any()) { Console.WriteLine("        >> " + _pv.GetValidator().PrintError(pv)); } 
        }

        public void PVValidation8(int pvId)
        {
            Console.WriteLine("     [PV 8] Confirm PV2 Michaelangelo");
            PaymentVoucher pv = _pv.ConfirmObject(_pv.GetObjectById(pvId), _pvd, _cb, _payable, _c);
            if (pv.Errors.Any()) { Console.WriteLine("        >> " + _pv.GetValidator().PrintError(pv)); }

        }

        public void PVValidation9(int pvdId)
        {
            Console.WriteLine("     [PV 9] Clear Payment Voucher Detail with Bank for Michaelangelo");
            PaymentVoucherDetail pvd = _pvd.ClearObject(_pvd.GetObjectById(pvdId), _pv, _cb, _payable, _c);
            if (pvd.Errors.Any()) { Console.WriteLine("        >> " + _pvd.GetValidator().PrintError(pvd)); }
        }

        public void PVValidation10(int pvdId)
        {
            Console.WriteLine("     [PV 10] Clear Payment Voucher Detail with non-Bank for Michaelangelo");
            PaymentVoucherDetail pvd = _pvd.ClearObject(_pvd.GetObjectById(pvdId), _pv, _cb, _payable, _c);
            if (pvd.Errors.Any()) { Console.WriteLine("        >> " + _pvd.GetValidator().PrintError(pvd)); }
        }
    }
}
