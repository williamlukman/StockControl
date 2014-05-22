using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IStockMutationService
    {
         IList<StockMutation> GetAll();
         IList<StockMutation> GetObjectsByItemId(int itemId);
         StockMutation GetObjectById(int Id);
         StockMutation CreateObject(StockMutation stockMutation);
         StockMutation UpdateObject(StockMutation stockMutation);
         StockMutation SoftDeleteObject(StockMutation stockMutation);
         bool DeleteObject(int Id);
         StockMutation CreateStockMutationForPurchaseOrder(PurchaseOrderDetail pod, Item item);
         IList<StockMutation> CreateStockMutationForPurchaseReceival(PurchaseReceivalDetail prd, Item item);
         StockMutation CreateStockMutationForSalesOrder(SalesOrderDetail sod, Item item);
         IList<StockMutation> CreateStockMutationForDeliveryOrder(DeliveryOrderDetail dod, Item item);
    }
}
