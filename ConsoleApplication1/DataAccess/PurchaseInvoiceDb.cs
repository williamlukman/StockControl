using Core.DomainModel;
using Core.Interface.Service;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataAccess
{
    class PurchaseInvoiceDb
    {

        public static void Delete(StockControlEntities db, IPurchaseInvoiceService _pi, IPurchaseInvoiceDetailService _pid)
        {
            var invoices = _pi.GetAll();

            foreach (var item in invoices)
            {
                var details = _pid.GetObjectsByPurchaseInvoiceId(item.Id);
                foreach (var detail in details)
                {
                    _pid.DeleteObject(detail.Id);
                }
                _pi.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IPurchaseInvoiceService _pi, IPurchaseInvoiceDetailService _pid)
        {
            var invoices = _pi.GetAll();

            Console.WriteLine("All invoices in the database:");
            int i = 0;
            foreach (var item in invoices)
            {
                Console.WriteLine(i++ + " " + item.Code);
            }
            Console.WriteLine();
        }
    }
}
