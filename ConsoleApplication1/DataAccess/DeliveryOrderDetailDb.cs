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
    class DeliveryOrderDetailDb
    {
        public static void Delete(StockControlEntities db, IDeliveryOrderDetailService _dod, int deliveryOrderId)
        {
            var deliveryOrders = _dod.GetObjectsByDeliveryOrderId(deliveryOrderId);
            Console.WriteLine("Delete all " + deliveryOrders.Count() + " previous deliveryOrders");

            foreach (var item in deliveryOrders)
            {
                _dod.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IDeliveryOrderDetailService _dod, int deliveryOrderId)
        {
            var deliveryOrders = _dod.GetObjectsByDeliveryOrderId(deliveryOrderId);

            Console.WriteLine("All deliveryOrders in the database:");
            foreach (var item in deliveryOrders)
            {
                Console.WriteLine("DOD ID: " + item.Id + ", Item: " + item.ItemId + ", Quantity: " + item.Quantity);
            }
            Console.WriteLine();
        }
    }
}
