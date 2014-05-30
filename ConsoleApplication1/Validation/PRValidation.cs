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
    public class PRValidation
    {
        private PurchaseReceivalValidator prv;
        private PurchaseReceivalDetailValidator prdv;
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

        public PRValidation(PurchaseReceivalValidator _prv, PurchaseReceivalDetailValidator _prdv, IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            prv = _prv;
            prdv = _prdv;
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
        }

        public void PRValidation20()
        {
            Console.WriteLine("[Test 20] Create valid Purchase Receival for Michaelangelo");
            PurchaseReceival pr = _pr.CreateObject(_c.GetObjectByName("Michaelangelo Buanorotti").Id, DateTime.Now,_c);
            if (pr.Errors.Any()) { Console.WriteLine(_pr.GetValidator().PrintError(pr)); }
        }

        public void PRValidation21(int purchaseOrderDetailId)
        {
            Console.WriteLine("[Test 21] Create valid Purchase Receival Detail for Michaelangelo");
            int prid = _pr.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id;
            PurchaseReceivalDetail prd1 = _prd.CreateObject(prid, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 100, purchaseOrderDetailId, _pr, _pod, _po, _i, _c);
            if (prd1.Errors.Any()) { Console.WriteLine(_prd.GetValidator().PrintError(prd1)); }
        }

        public void PRValidation22()
        {
            Console.WriteLine("[Test 22] Create invalid Purchase Receival (wrong contact id)");
            PurchaseReceival pr = _pr.CreateObject(0, DateTime.Now, _c);
            if (pr.Errors.Any()) { Console.WriteLine(_pr.GetValidator().PrintError(pr)); }
        }

        public void PRValidation23()
        {
            Console.WriteLine("[Test 23] Create valid Purchase Receival");
            PurchaseReceival pr = _pr.CreateObject(_c.GetObjectByName("Andy Robinson").Id, new DateTime(2000, 2, 28), _c);
            if (pr.Errors.Any()) { Console.WriteLine(_pr.GetValidator().PrintError(pr)); }
        }

        public void PRValidation24(int purchaseOrderDetailId)
        {
            Console.WriteLine("[Test 24] Create invalid Purchase Receival Detail for Michaelangelo with wrong contact");
            PurchaseReceivalDetail prd1 = _prd.CreateObject(0, _i.GetObjectByName("Mini Garuda Indonesia").Id, 100, purchaseOrderDetailId, _pr, _pod, _po, _i, _c);
            if (prd1.Errors.Any()) { Console.WriteLine(_prd.GetValidator().PrintError(prd1)); }
        }

        public void PRValidation25(int purchaseOrderDetailId)
        {
            Console.WriteLine("[Test 25] Create invalid Purchase Receival Detail for Michaelangelo with exact same item");
            PurchaseReceivalDetail prd1 = _prd.CreateObject(_pr.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 50, purchaseOrderDetailId, _pr, _pod, _po, _i, _c);
            if (prd1.Errors.Any()) { Console.WriteLine(_prd.GetValidator().PrintError(prd1)); }
        }

        public void PRValidation26(int purchaseOrderDetailId)
        {
            Console.WriteLine("[Test 26] Create valid Purchase Receival Detail for Michaelangelo");
            PurchaseReceivalDetail prd1 = _prd.CreateObject(_pr.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Mini Garuda Indonesia").Id, 50, purchaseOrderDetailId, _pr, _pod, _po, _i, _c);
            if (prd1.Errors.Any()) { Console.WriteLine(_prd.GetValidator().PrintError(prd1)); }
        }

        public void PRValidation27()
        {
            Console.WriteLine("[Test 27] Confirm PR and PRD for Michaelangelo");
            PurchaseReceival pr = _pr.ConfirmObject(_pr.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _prd, _pod, _sm, _i);
            if (pr.Errors.Any()) { Console.WriteLine(_pr.GetValidator().PrintError(pr)); }
        }

    }
}
