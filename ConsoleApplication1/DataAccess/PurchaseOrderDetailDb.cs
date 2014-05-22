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
    class PurchaseOrderDetailDb
    {
        public static PurchaseOrderDetail CreatePOD(StockControlEntities db, IPurchaseOrderDetailService _pods, int PurchaseOrderId, int ItemId, int quantity)
        {

            // Fill DB
            PurchaseOrderDetail pod = new PurchaseOrderDetail
            {
                PurchaseOrderId = PurchaseOrderId,
                ItemId = ItemId,
                Quantity = quantity,
            };
            pod = _pods.CreateObject(pod);
            pod.Id = pod.Id;
            return pod;
        }

        public static void Delete(StockControlEntities db, IPurchaseOrderDetailService _pod)
        {
            IPurchaseReceivalDetailRepository _prdrepo = new PurchaseReceivalDetailRepository();
            var purchaseOrderDetails = _prdrepo.FindAll();
            Console.WriteLine("Delete all " + purchaseOrderDetails.Count() + " previous purchaseOrderDetails");

            foreach (var item in purchaseOrderDetails)
            {
                _pod.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IPurchaseOrderDetailService _pod, int purchaseOrderId)
        {
            var purchaseOrderDetails = _pod.GetObjectsByPurchaseOrderId(purchaseOrderId);
            Console.WriteLine("All purchaseOrderDetails in the database:");

            foreach (var item in purchaseOrderDetails)
            {
                Console.WriteLine("POD ID: " + item.Id + ", Item Id: " + item.ItemId + ", Quantity:" + item.Quantity );
            }
            Console.WriteLine();
        }
    }
}
