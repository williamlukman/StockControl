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
    class SalesOrderDb
    {

        public static void Delete(StockControlEntities db, ISalesOrderService _so, ISalesOrderDetailService _sod)
        {
            var salesOrders = _so.GetAll();

            foreach (var item in salesOrders)
            {
                _so.DeleteObject(item.Id);
                var salesOrderDetails = _sod.GetObjectsBySalesOrderId(item.Id);
                foreach (var detailitem in salesOrderDetails)
                {
                    _sod.DeleteObject(detailitem.Id);
                }

            }
        }

        public static void Display(StockControlEntities db, ISalesOrderService _c)
        {
            var salesOrders = _c.GetAll();

            Console.WriteLine("All salesOrders in the database:");
            foreach (var item in salesOrders)
            {
                Console.WriteLine("SO ID: " + item.Id + ", PO Sales Date: " + item.SalesDate);
            }
        }
    }
}
