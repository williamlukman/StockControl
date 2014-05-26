using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IStockMutationService
    {
         IStockMutationValidator GetValidator();
         IList<StockMutation> GetAll();
         IList<StockMutation> GetObjectsByItemId(int itemId);
         StockMutation GetObjectById(int Id);
         IList<StockMutation> GetObjectsBySourceDocumentDetail(int itemId, string SourceDocumentDetailType, int SourceDocumentDetailId);
         StockMutation CreateObject(StockMutation stockMutation);
         StockMutation UpdateObject(StockMutation stockMutation);
         StockMutation SoftDeleteObject(StockMutation stockMutation);
         bool DeleteObject(int Id);
         StockMutation CreateStockMutationForPurchaseOrder(PurchaseOrderDetail pod, Item item);
         IList<StockMutation> CreateStockMutationForPurchaseReceival(PurchaseReceivalDetail prd, Item item);
         StockMutation CreateStockMutationForSalesOrder(SalesOrderDetail sod, Item item);
         IList<StockMutation> CreateStockMutationForDeliveryOrder(DeliveryOrderDetail dod, Item item);
         IList<StockMutation> SoftDeleteStockMutationForPurchaseOrder(PurchaseOrderDetail pod, Item item);
         IList<StockMutation> SoftDeleteStockMutationForPurchaseReceival(PurchaseReceivalDetail prd, Item item);
         IList<StockMutation> SoftDeleteStockMutationForSalesOrder(SalesOrderDetail sod, Item item);
         IList<StockMutation> SoftDeleteStockMutationForDeliveryOrder(DeliveryOrderDetail dod, Item item);

    }
}
