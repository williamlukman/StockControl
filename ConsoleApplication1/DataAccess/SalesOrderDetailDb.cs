using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Data.Context;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataAccess
{
    class SalesOrderDetailDb
    {
        public static void Delete(StockControlEntities db, ISalesOrderDetailService _sod, int salesOrderId)
        {
            var salesOrderDetails = _sod.GetObjectsBySalesOrderId(salesOrderId);
            Console.WriteLine("Delete all " + salesOrderDetails.Count() + " previous salesOrderDetails");

            foreach (var item in salesOrderDetails)
            {
                _sod.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, ISalesOrderDetailService _sod, int salesOrderId)
        {
            var salesOrderDetails = _sod.GetObjectsBySalesOrderId(salesOrderId);
            Console.WriteLine("All salesOrderDetails in the database:");

            foreach (var item in salesOrderDetails)
            {
                Console.WriteLine("SOD ID: " + item.Id + ", Item Id: " + item.ItemId + ", Quantity:" + item.Quantity );
            }
            Console.WriteLine();
        }
    }
}
