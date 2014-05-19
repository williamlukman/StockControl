using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class StockMutationService : IStockMutationService
    {
        public IList<StockMutation> GetAll(IStockMutationRepository _sm)
        {
            return _sm.GetAll();
        }

        public IList<StockMutation> GetObjectsByItemId(int itemId, IStockMutationRepository _sm)
        {
            return _sm.GetObjectsByItemId(itemId);
        }

        public StockMutation GetObjectById(int Id, IStockMutationRepository _sm)
        {
            return _sm.GetObjectById(Id);
        }

        public StockMutation CreateObject(StockMutation stockMutation, IStockMutationRepository _sm)
        {
            return _sm.CreateObject(stockMutation);
        }

        public StockMutation UpdateObject(StockMutation stockMutation, IStockMutationRepository _sm)
        {
            return _sm.UpdateObject(stockMutation);
        }

        public StockMutation SoftDeleteObject(StockMutation stockMutation, IStockMutationRepository _sm)
        {
            return _sm.SoftDeleteObject(stockMutation);
        }

        public bool DeleteObject(int Id, IStockMutationRepository _sm)
        {
            return _sm.DeleteObject(Id);
        }

        public StockMutation CreateStockMutationForPurchaseOrder(PurchaseOrderDetail pod, Item item, IStockMutationRepository _sm)
        {
            StockMutation sm = new StockMutation();
            sm.ItemId = pod.ItemId;
            sm.Quantity = pod.Quantity;
            sm.SourceDocumentType = "PurchaseOrder";
            sm.SourceDocumentId = pod.PurchaseOrderId;
            sm.SourceDocumentDetailType = "PurchaseOrderDetail";
            sm.SourceDocumentDetailId = pod.Id;
            sm.ItemCase = Constant.StockMutationItemCase.PendingReceival;
            sm.Status = Constant.StockMutationStatus.Addition;
            sm.CreatedAt = DateTime.Now;
            sm.IsDeleted = false;
            return _sm.CreateObject(sm);
            
        }
    }
}
