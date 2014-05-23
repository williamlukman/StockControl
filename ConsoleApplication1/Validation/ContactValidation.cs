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
            Contact contact = _c.CreateObject("Michaelangelo Buanorotti", "Pisa Tower");
            Console.WriteLine("1. Test valid contact Michaelangelo");
            Console.WriteLine(cv.ValidCreateObject(contact) ? "Success." : "Fail. Error message: " + cv.PrintError(contact)); 
        }

        public void ContactValidation2()
        {
            Contact contact = _c.CreateObject("    ", "Empty space name does not live in anywhere");
            Console.WriteLine("2. Test empty string name");
            Console.WriteLine(!cv.ValidCreateObject(contact) ? "Success. Error message: " + cv.PrintError(contact) : "Fail. Not caught by isValid");
        }

        public void ContactValidation3()
        {
            Contact contact = _c.CreateObject("I have no address.", " ");
            Console.WriteLine("3. Test empty string address");
            Console.WriteLine(!cv.ValidCreateObject(contact) ? "Success. Error message: " + cv.PrintError(contact) : "Fail. Not caught by isValid");
        }

        public void ContactValidation4()
        {
            Contact contact = _c.CreateObject("Andy Robinson", "CEO of Gotong Royong");
            Console.WriteLine("4. Test Delete contact with no POs, PRs, SOs, DOs");
            Console.WriteLine(cv.ValidDeleteObject(contact, _po, _pr, _so, _do) ? "Success." : "Fail.  Error message: " + cv.PrintError(contact));
        }


        public void ContactValidation5()
        {
            Contact contact = _c.CreateObject("Andy Barbie", "CEO of Putus Asa");
            PurchaseOrder po1 = _po.CreateObject(_c.GetObjectByName("Andy Barbie").Id, DateTime.Today);
            Console.WriteLine("5. Test Delete contact with PO");
            Console.WriteLine(!cv.ValidDeleteObject(contact, _po, _pr, _so, _do) ? "Success. Error message: " + cv.PrintError(contact) : "Fail.");
        }

        public void ContactValidation6()
        {
            Contact contact = _c.CreateObject("Andy Upho", "CEO of Angkat Besi");
            SalesOrder so1 = _so.CreateObject(_c.GetObjectByName("Andy Upho").Id, DateTime.Today);
            Console.WriteLine("6. Test Delete contact with SO");
            Console.WriteLine(!cv.ValidDeleteObject(contact, _po, _pr, _so, _do) ? "Success. Error message: " + cv.PrintError(contact) : "Fail.");        
        }
    }
}
