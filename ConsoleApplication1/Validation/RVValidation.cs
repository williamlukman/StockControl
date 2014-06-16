
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
    public class RVValidation
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
        private ISalesInvoiceService _pi;
        private ISalesInvoiceDetailService _pid;
        private IReceivableService _receivable;
        private IReceiptVoucherService _rv;
        private IReceiptVoucherDetailService _rvd;
        private ICashBankService _cb;

        public RVValidation(     ISalesInvoiceService si, ISalesInvoiceDetailService sid,
                                 IReceivableService receivable, IReceiptVoucherService rv, IReceiptVoucherDetailService rvd,
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
            _pi = si;
            _pid = sid;
            _rv = rv;
            _rvd = rvd;
            _receivable = receivable;
            _cb = cb;
        }

        public int ReceivableValidation1(int piId)
        {
            Console.WriteLine("     [RV 1] Create valid Receivable 10jt for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            Receivable receivable = _receivable.CreateObject(c.Id, "SalesInvoice", piId, 10000000);
            if (receivable.Errors.Any()) { Console.WriteLine("        >> " + _receivable.GetValidator().PrintError(receivable)); return 0; }
            return receivable.Id;
        }

        public int ReceivableValidation2(int piId)
        {
            Console.WriteLine("     [RV 2] Create valid Receivable 5jt for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            Receivable receivable = _receivable.CreateObject(c.Id, "SalesInvoice", piId, 5000000);
            if (receivable.Errors.Any()) { Console.WriteLine("        >> " + _receivable.GetValidator().PrintError(receivable)); return 0; }
            return receivable.Id;
        }


        public int RVValidation1()
        {
            Console.WriteLine("     [RV 1] Create valid Receipt Voucher by Bank for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            CashBank cb = _cb.GetObjectByName("Mandiri");
            ReceiptVoucher rv = _rv.CreateObject(cb.Id, c.Id, DateTime.Today, 20000000, _rvd, _receivable, _c, _cb);
            if (rv.Errors.Any()) { Console.WriteLine("        >> " + _rv.GetValidator().PrintError(rv)); return 0; }
            return rv.Id;
        }

        public int RVValidation2a(int rvID, int receivableId)
        {
            Console.WriteLine("     [RV 2a] Create valid Receipt Voucher Detail for Michaelangelo");
            ReceiptVoucherDetail rvd = _rvd.CreateObject(rvID, receivableId, (decimal) 3000000, "Receipt 3jt untuk Receivable 10jt", false, _rv, _cb, _receivable, _c);
            if (rvd.Errors.Any()) { Console.WriteLine("        >> " + _rvd.GetValidator().PrintError(rvd)); return 0; }
            return rvd.Id;
        }

        public int RVValidation2b(int rvID, int receivableId)
        {
            Console.WriteLine("     [RV 2b] Create valid Receipt Voucher Detail for Michaelangelo");
            ReceiptVoucherDetail rvd = _rvd.CreateObject(rvID, receivableId, (decimal)2000000, "Receipt 2jt untuk Receivable 5jt", false, _rv, _cb, _receivable, _c);
            if (rvd.Errors.Any()) { Console.WriteLine("        >> " + _rvd.GetValidator().PrintError(rvd)); return 0; }
            return rvd.Id;
        }

        public int RVValidation2c(int rvID, int receivableId)
        {
            Console.WriteLine("     [RV 2c] Create valid Receipt Voucher Detail for Michaelangelo");
            ReceiptVoucherDetail rvd = _rvd.CreateObject(rvID, receivableId, (decimal)4000000, "Receipt 4jt untuk Receivable 5jt", false, _rv, _cb, _receivable, _c);
            if (rvd.Errors.Any()) { Console.WriteLine("        >> " + _rvd.GetValidator().PrintError(rvd)); return 0; }
            return rvd.Id;
        }

        public int RVValidation4()
        {
            Console.WriteLine("     [RV 4] Create valid Receipt Voucher by Cash for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            CashBank cb = _cb.GetObjectByName("SSD PettyCash");
            ReceiptVoucher rv = _rv.CreateObject(cb.Id, c.Id, DateTime.Today, 7500000, _rvd, _receivable, _c, _cb);
            if (rv.Errors.Any()) { Console.WriteLine("        >> " + _rv.GetValidator().PrintError(rv)); return 0; }
            return rv.Id;
        }

        public int RVValidation5(int rvID, int receivableId)
        {
            Console.WriteLine("     [RV 5] Create valid Receipt Voucher Detail for Michaelangelo");
            ReceiptVoucherDetail rvd = _rvd.CreateObject(rvID, receivableId, (decimal)4000000, "Receipt 4jt untuk Receivable 10jt", true, _rv, _cb, _receivable, _c);
            if (rvd.Errors.Any()) { Console.WriteLine("        >> " + _rvd.GetValidator().PrintError(rvd)); return 0; }
            return rvd.Id;
        }

        public int RVValidation6(int rvID, int receivableId)
        {
            Console.WriteLine("     [RV 6] Create valid Receipt Voucher Detail for Michaelangelo");
            ReceiptVoucherDetail rvd = _rvd.CreateObject(rvID, receivableId, (decimal)3500000, "Receipt 3.5jt untuk Receivable 5jt", true, _rv, _cb, _receivable, _c);
            if (rvd.Errors.Any()) { Console.WriteLine("        >> " + _rvd.GetValidator().PrintError(rvd)); return 0; }
            return rvd.Id;
        }

        public void RVValidation7(int rvID)
        {
            Console.WriteLine("     [RV 7] Confirm RV1 Michaelangelo");
            ReceiptVoucher rv = _rv.ConfirmObject(_rv.GetObjectById(rvID), _rvd, _cb, _receivable, _c);
            if (rv.Errors.Any()) { Console.WriteLine("        >> " + _rv.GetValidator().PrintError(rv)); } 
        }

        public void RVValidation8(int rvID)
        {
            Console.WriteLine("     [RV 8] Confirm RV2 Michaelangelo");
            ReceiptVoucher rv = _rv.ConfirmObject(_rv.GetObjectById(rvID), _rvd, _cb, _receivable, _c);
            if (rv.Errors.Any()) { Console.WriteLine("        >> " + _rv.GetValidator().PrintError(rv)); }

        }

        public void RVValidation9(int rvdId)
        {
            Console.WriteLine("     [RV 9] Clear Receipt Voucher Detail with Bank for Michaelangelo");
            ReceiptVoucherDetail rvd = _rvd.ClearObject(_rvd.GetObjectById(rvdId), _rv, _cb, _receivable, _c);
            if (rvd.Errors.Any()) { Console.WriteLine("        >> " + _rvd.GetValidator().PrintError(rvd)); }
        }

        public void RVValidation10(int rvdId)
        {
            Console.WriteLine("     [RV 10] Clear Receipt Voucher Detail with non-Bank for Michaelangelo");
            ReceiptVoucherDetail rvd = _rvd.ClearObject(_rvd.GetObjectById(rvdId), _rv, _cb, _receivable, _c);
            if (rvd.Errors.Any()) { Console.WriteLine("        >> " + _rvd.GetValidator().PrintError(rvd)); }
        }
    }
}
