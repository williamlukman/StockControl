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
    public class ContactValidation
    {
        private ContactValidator cv;
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

        public ContactValidation(ContactValidator _cv, IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            cv = _cv;
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

        public void ContactValidation1()
        {
            Console.WriteLine("[Test 1] Create valid person Michaelangelo");
            Contact contact = _c.CreateObject("Michaelangelo Buanorotti", "Pisa Tower");
            if (contact.Errors.Any()) { Console.WriteLine(_c.GetValidator().PrintError(contact)); }
        }

        public void ContactValidation2()
        {
            Console.WriteLine("[Test 2] Create invalid person with empty string name");
            Contact contact = _c.CreateObject("    ", "Empty space name does not live in anywhere");
            if (contact.Errors.Any()) { Console.WriteLine(_c.GetValidator().PrintError(contact)); }
        }

        public void ContactValidation3()
        {
            Console.WriteLine("[Test 3] Create invalid person with empty string address");
            Contact contact = _c.CreateObject("I have no address.", " ");
            if (contact.Errors.Any()) { Console.WriteLine(_c.GetValidator().PrintError(contact)); }
        }

        public void ContactValidation4()
        {
            Console.WriteLine("[Test 4] Create valid person Andy Robinson");
            Contact contact = _c.CreateObject("Andy Robinson", "CEO of Gotong Royong");
            if (contact.Errors.Any()) { Console.WriteLine(_c.GetValidator().PrintError(contact)); }
        }

        public void ContactValidation5()
        {
            Console.WriteLine("[Test 5] Delete Person Andy Barbie with associated purchase order");
            Contact contact = _c.CreateObject("Andy Barbie", "CEO of Putus Asa");
            PurchaseOrder po1 = _po.CreateObject(_c.GetObjectByName("Andy Barbie").Id, DateTime.Today, _c);
            contact =_c.SoftDeleteObject(contact, _po, _pr, _so, _do);
            if (po1.Errors.Any()) { Console.WriteLine(_po.GetValidator().PrintError(po1)); }
            if (contact.Errors.Any()) { Console.WriteLine(_c.GetValidator().PrintError(contact)); }
        }

        public void ContactValidation6()
        {
            Console.WriteLine("[Test 6] Delete Person Andy Upho with associated sales order");
            Contact contact = _c.CreateObject("Andy Upho", "CEO of Angkat Besi");
            SalesOrder so1 = _so.CreateObject(_c.GetObjectByName("Andy Upho").Id, DateTime.Today, _c);
            contact = _c.SoftDeleteObject(contact, _po, _pr, _so, _do);
            if (contact.Errors.Any()) { Console.WriteLine(_c.GetValidator().PrintError(contact)); }
            if (so1.Errors.Any()) { Console.WriteLine(_so.GetValidator().PrintError(so1)); }
        }
    }
}
