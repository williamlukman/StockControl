using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IStockMutationService
    {
        public IList<StockMutation> GetAll();
        public IList<StockMutation> GetObjectsByItemId(int itemId);
        public StockMutation GetObjectById(int Id);
        public StockMutation CreateObject(StockMutation stockMutation);
        public StockMutation UpdateObject(StockMutation stockMutation);
        public StockMutation SoftDeleteObject(StockMutation stockMutation);
        public bool DeleteObject(int Id);

        public StockMutation CreateStockMutationForPurchaseOrder(PurchaseOrderDetail pod, Item item);
    }
}
