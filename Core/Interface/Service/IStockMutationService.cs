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
        public IList<StockMutation> GetAll(IStockMutationRepository _sm);
        public IList<StockMutation> GetObjectsByItemId(int itemId, IStockMutationRepository _sm);
        public StockMutation GetObjectById(int Id, IStockMutationRepository _sm);
        public StockMutation CreateObject(StockMutation stockMutation, IStockMutationRepository _sm);
        public StockMutation UpdateObject(StockMutation stockMutation, IStockMutationRepository _sm);
        public StockMutation SoftDeleteObject(StockMutation stockMutation, IStockMutationRepository _sm);
        public bool DeleteObject(int Id, IStockMutationRepository _sm);
        public StockMutation CreateStockMutationForPurchaseOrder(PurchaseOrderDetail pod, Item item, IStockMutationRepository _sm);
    }
}
