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
            Console.WriteLine("     [Item 1] Create valid item Buku Tulis Kiky");
            Item item = _i.CreateObject("Buku Tulis Kiky A5", "Buku Tulis Garis-Garis Anak Sekolah", "KIKY0001");
            if (item.Errors.Any()) { Console.WriteLine("        >> " + _i.GetValidator().PrintError(item)); }
        }

        public void ItemValidation2()
        {
            Console.WriteLine("     [Item 2] Create item empty space");
            Item item = _i.CreateObject("    ", "Empty space name description", "SUPER0001");
            if (item.Errors.Any()) { Console.WriteLine("        >> " + _i.GetValidator().PrintError(item)); }
        }

        public void ItemValidation3()
        {
            Console.WriteLine("     [Item 3] Create valid item Mini Garuda Indonesia");
            Item item = _i.CreateObject("Mini Garuda Indonesia", "Mini Pesawat Garuda Indonesia dengan stool", "SUPER0001");
            if (item.Errors.Any()) { Console.WriteLine("        >> " + _i.GetValidator().PrintError(item)); }
        }

        public void ItemValidation4()
        {
            Console.WriteLine("     [Item 4] Soft Delete valid item");
            Item item = _i.CreateObject("Buku Berlayar", "Berlayar Mengarungi Samudera Hindia dalam setahun", "LAY0001");
            _i.SoftDeleteObject(item, _sm);
            if (item.Errors.Any()) { Console.WriteLine("        >> " + _i.GetValidator().PrintError(item)); }
        }

        public void ItemValidation5()
        {
            Console.WriteLine("     [Item 5] Delete item Masak Memasak with associated SO");
            Contact contact = _c.CreateObject("Chef Degan", "I'm interested in all cooking books");
            Item item = _i.CreateObject("Masak Memasak Koki Ternama", "Master Chef Junior Learning from World Chefs", "COOK1234");
            SalesOrder so = _so.CreateObject(contact.Id, DateTime.Today, _c);
            SalesOrderDetail sod = _sod.CreateObject(so.Id, item.Id, 1, (decimal) 5000.00, _so, _i);
            so.ConfirmedAt = new DateTime(2014, 5, 6);
            so = _so.ConfirmObject(so, _sod, _sm, _i);
            _i.SoftDeleteObject(item, _sm);
            if (contact.Errors.Any()) { Console.WriteLine("        >> " + _c.GetValidator().PrintError(contact)); }
            if (item.Errors.Any()) { Console.WriteLine("        >> " + _i.GetValidator().PrintError(item)); }
            if (so.Errors.Any()) { Console.WriteLine("        >> " + _so.GetValidator().PrintError(so)); }
            if (sod.Errors.Any()) { Console.WriteLine("        >> " + _sod.GetValidator().PrintError(sod)); }
        }
    }
}
