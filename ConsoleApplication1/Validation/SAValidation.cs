using ConsoleApp.DataAccess;
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
    public class SAValidation
    {
        private StockAdjustmentValidator sav;
        private StockAdjustmentDetailValidator sadv;
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
        private IStockAdjustmentService _sa;
        private IStockAdjustmentDetailService _sad;

        public SAValidation(StockAdjustmentValidator _sav, StockAdjustmentDetailValidator _sadv, IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod,
                                 IStockAdjustmentService sa, IStockAdjustmentDetailService sad)
        {
            sav = _sav;
            sadv = _sadv;
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
            _sa = sa;
            _sad = sad;
        }

        public int SAValidation1()
        {
            Console.WriteLine("     [SA 1] Create valid Stock Adjustment");
            StockAdjustment sa = _sa.CreateObject( DateTime.Now);
            if (sa.Errors.Any()) { Console.WriteLine("        >> " + _sa.GetValidator().PrintError(sa)); return 0; }
            return sa.Id;
        }

        public void SAValidation2(int stockAdjustmentId)
        {
            Console.WriteLine("     [SA 2] Create valid SAD");
            StockAdjustmentDetail sad = _sad.CreateObject(stockAdjustmentId, _i.GetObjectByName("Mini Garuda Indonesia").Id, 1000, 100000, _sa, _i);
            if (sad.Errors.Any()) { Console.WriteLine("        >> " + _sad.GetValidator().PrintError(sad)); }
        }

        public void SAValidation3(int stockAdjustmentId)
        {
            Console.WriteLine("     [SA 3] Confirm SA");
            StockAdjustment sa = _sa.ConfirmObject(_sa.GetObjectById(stockAdjustmentId), _sad, _sm, _i);
            if (sa.Errors.Any()) { Console.WriteLine("        >> " + _sa.GetValidator().PrintError(sa)); }
        }

        public void SAValidation4(int stockAdjustmentId)
        {
            Console.WriteLine("     [SA 4] Unconfirm SA");
            StockAdjustment sa = _sa.UnconfirmObject(_sa.GetObjectById(stockAdjustmentId), _sad, _sm, _i);
            if (sa.Errors.Any()) { Console.WriteLine("        >> " + _sa.GetValidator().PrintError(sa)); }
        }

        public int SAValidation5()
        {
            Console.WriteLine("     [SA 5] Create second valid Stock Adjustment");
            StockAdjustment sa = _sa.CreateObject( DateTime.Now);
            if (sa.Errors.Any()) { Console.WriteLine("        >> " + _sa.GetValidator().PrintError(sa)); return 0; }
            return sa.Id;
        }

        public void SAValidation6(int stockAdjustmentId)
        {
            Console.WriteLine("     [SA 6] Create negative quantity SAD");
            StockAdjustmentDetail sad = _sad.CreateObject(stockAdjustmentId, _i.GetObjectByName("Mini Garuda Indonesia").Id, -300, 100000, _sa, _i);
            if (sad.Errors.Any()) { Console.WriteLine("        >> " + _sad.GetValidator().PrintError(sad)); }
        }

        public void SAValidation7(int stockAdjustmentId)
        {
            Console.WriteLine("     [SA 7] Create zero quantity SAD");
            StockAdjustmentDetail sad = _sad.CreateObject(stockAdjustmentId, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 0, 30000, _sa, _i);
            if (sad.Errors.Any()) { Console.WriteLine("        >> " + _sad.GetValidator().PrintError(sad)); }
        }

        public void SAValidation8(int stockAdjustmentId)
        {
            Console.WriteLine("     [SA 8] Create duplicate item SAD");
            StockAdjustmentDetail sad = _sad.CreateObject(stockAdjustmentId, _i.GetObjectByName("Mini Garuda Indonesia").Id, 50, 30000, _sa, _i);
            if (sad.Errors.Any()) { Console.WriteLine("        >> " + _sad.GetValidator().PrintError(sad)); }
        }

        public void SAValidation9(int stockAdjustmentId)
        {
            Console.WriteLine("     [SA 9] Invalid confirm SA2 with negative quantity");
            StockAdjustment sa = _sa.ConfirmObject(_sa.GetObjectById(stockAdjustmentId), _sad, _sm, _i);
            if (sa.Errors.Any()) { Console.WriteLine("        >> " + _sa.GetValidator().PrintError(sa)); }
        }

        public void SAValidation10(int stockAdjustmentId)
        {
            Console.WriteLine("     [SA 10] Valid confirm SA2 with negative quantity");
            StockAdjustment sa = _sa.ConfirmObject(_sa.GetObjectById(stockAdjustmentId), _sad, _sm, _i);
            if (sa.Errors.Any()) { Console.WriteLine("        >> " + _sa.GetValidator().PrintError(sa)); }
        }
    }
}
