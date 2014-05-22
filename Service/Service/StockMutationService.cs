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
        private IStockMutationRepository _sm;
        public StockMutationService(IStockMutationRepository _stockMutationRepository)
        {
            _sm = _stockMutationRepository;
        }

        public IList<StockMutation> GetAll()
        {
            return _sm.GetAll();
        }

        public IList<StockMutation> GetObjectsByItemId(int itemId)
        {
            return _sm.GetObjectsByItemId(itemId);
        }

        public StockMutation GetObjectById(int Id)
        {
            return _sm.GetObjectById(Id);
        }

        public StockMutation CreateObject(StockMutation stockMutation)
        {
            return _sm.CreateObject(stockMutation);
        }

        public StockMutation UpdateObject(StockMutation stockMutation)
        {
            return _sm.UpdateObject(stockMutation);
        }

        public StockMutation SoftDeleteObject(StockMutation stockMutation)
        {
            return _sm.SoftDeleteObject(stockMutation);
        }

        public bool DeleteObject(int Id)
        {
            return _sm.DeleteObject(Id);
        }

        public StockMutation CreateStockMutationForPurchaseOrder(PurchaseOrderDetail pod, Item item)
        {
            StockMutation sm = new StockMutation();
            sm.ItemId = pod.ItemId;
            sm.Quantity = pod.Quantity;
            sm.SourceDocumentType = Constant.SourceDocumentType.PurchaseOrder;
            sm.SourceDocumentId = pod.PurchaseOrderId;
            sm.SourceDocumentDetailType = Constant.SourceDocumentDetailType.PurchaseOrderDetail;
            sm.SourceDocumentDetailId = pod.Id;
            sm.ItemCase = Constant.StockMutationItemCase.PendingReceival;
            sm.Status = Constant.StockMutationStatus.Addition;
            return _sm.CreateObject(sm);
        }

        public IList<StockMutation> CreateStockMutationForPurchaseReceival(PurchaseReceivalDetail prd, Item item)
        {
            IList<StockMutation> result = new List<StockMutation>();
            
            StockMutation sm = new StockMutation();
            sm.ItemId = prd.ItemId;
            sm.Quantity = prd.Quantity;
            sm.SourceDocumentType = Constant.SourceDocumentType.PurchaseReceival;
            sm.SourceDocumentId = prd.PurchaseReceivalId;
            sm.SourceDocumentDetailType = Constant.SourceDocumentDetailType.PurchaseReceivalDetail;
            sm.SourceDocumentDetailId = prd.Id;
            sm.ItemCase = Constant.StockMutationItemCase.PendingReceival;
            sm.Status = Constant.StockMutationStatus.Deduction;
            result.Add(_sm.CreateObject(sm));

            sm.ItemCase = Constant.StockMutationItemCase.Ready;
            sm.Status = Constant.StockMutationStatus.Addition;
            result.Add(_sm.CreateObject(sm));
            return result;
        }

        public StockMutation CreateStockMutationForSalesOrder(SalesOrderDetail sod, Item item)
        {
            StockMutation sm = new StockMutation();
            sm.ItemId = sod.ItemId;
            sm.Quantity = sod.Quantity;
            sm.SourceDocumentType = Constant.SourceDocumentType.SalesOrder;
            sm.SourceDocumentId = sod.SalesOrderId;
            sm.SourceDocumentDetailType = Constant.SourceDocumentDetailType.SalesOrderDetail;
            sm.SourceDocumentDetailId = sod.Id;
            sm.ItemCase = Constant.StockMutationItemCase.PendingDelivery;
            sm.Status = Constant.StockMutationStatus.Addition;
            return _sm.CreateObject(sm);
        }

        public IList<StockMutation> CreateStockMutationForDeliveryOrder(DeliveryOrderDetail prd, Item item)
        {
            IList<StockMutation> result = new List<StockMutation>();

            StockMutation sm = new StockMutation();
            sm.ItemId = prd.ItemId;
            sm.Quantity = prd.Quantity;
            sm.SourceDocumentType = Constant.SourceDocumentType.DeliveryOrder;
            sm.SourceDocumentId = prd.DeliveryOrderId;
            sm.SourceDocumentDetailType = Constant.SourceDocumentDetailType.DeliveryOrderDetail;
            sm.SourceDocumentDetailId = prd.Id;
            sm.ItemCase = Constant.StockMutationItemCase.PendingDelivery;
            sm.Status = Constant.StockMutationStatus.Deduction;
            result.Add(_sm.CreateObject(sm));

            sm.ItemCase = Constant.StockMutationItemCase.Ready;
            sm.Status = Constant.StockMutationStatus.Deduction;
            result.Add(_sm.CreateObject(sm));
            return result;
        }
    }
}
