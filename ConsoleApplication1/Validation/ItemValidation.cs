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
    public class ItemValidation
    {
        private ItemValidator iv;
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

        public ItemValidation(ItemValidator _iv, IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            iv = _iv;
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

        public void ItemValidation1()
        {
            Item item = _i.CreateObject("Buku Tulis Kiky A5", "Buku Tulis Garis-Garis Anak Sekolah", "KIKY0001", 500);
            Console.WriteLine("1. Test valid item Buku Tulis Kiky A5");
            Console.WriteLine(iv.ValidCreateObject(item, _i) ? "Success." : "Fail. Error message: " + iv.PrintError(item)); 
        }

        public void ItemValidation2()
        {
            Item item = _i.CreateObject("    ", "Empty space name description", "SUPER0001", 200);
            Console.WriteLine("2. Test empty string name");
            Console.WriteLine(!iv.ValidCreateObject(item, _i) ? "Success. Error message: " + iv.PrintError(item) : "Fail. Not caught by isValid");
        }

        public void ItemValidation3()
        {
            Item item = _i.CreateObject("Mini Garuda Indonesia", "Mini Pesawat Garuda Indonesia dengan stool", "SUPER0001", 150);
            Console.WriteLine("3. Test duplicate Sku");
            Console.WriteLine(!iv.ValidCreateObject(item, _i) ? "Success. Error message: " + iv.PrintError(item) : "Fail. Not caught by isValid");
        }

        public void ItemValidation4()
        {
            Item item = _i.CreateObject("Buku Berlayar", "Berlayar Mengarungi Samudera Hindia dalam setahun", "LAY0001", 35);
            Console.WriteLine("4. Test Delete item with no SMs");
            Console.WriteLine(iv.ValidDeleteObject(item, _sm) ? "Success." : "Fail.  Error message: " + iv.PrintError(item));
        }

        public void ItemValidation5()
        {
            Contact contact = _c.CreateObject("Chef Degan", "I'm interested in all cooking books");
            Item item = _i.CreateObject("Masak Memasak Koki Ternama", "Master Chef Junior Learning from World Chefs", "COOK1234", 123);
            SalesOrder so = _so.CreateObject(contact.Id, DateTime.Today);
            SalesOrderDetail sod = _sod.CreateObject(so.Id, item.Id, 1);
            so = _so.ConfirmObject(so);
            sod = _sod.ConfirmObject(sod);
            StockMutation sm = _sm.CreateStockMutationForSalesOrder(sod, item);
            Console.WriteLine("5. Test Delete item with stock mutation");
            Console.WriteLine(!iv.ValidDeleteObject(item, _sm) ? "Success. Error message: " + iv.PrintError(item) : "Fail.");
        }
    }
}
