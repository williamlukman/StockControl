using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interface.Validation;

namespace Service.Service
{
    public class StockMutationService : IStockMutationService
    {
        private IStockMutationRepository _sm;
        private IStockMutationValidator _validator;

        public StockMutationService(IStockMutationRepository _stockMutationRepository, IStockMutationValidator _stockMutationValidator)
        {
            _sm = _stockMutationRepository;
            _validator = _stockMutationValidator;
        }

        public IStockMutationValidator GetValidator()
        {
            return _validator;
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

        public IList<StockMutation> GetObjectsBySourceDocumentDetail(int itemId, string SourceDocumentDetailType, int SourceDocumentDetailId)
        {
            return _sm.GetObjectsBySourceDocumentDetail(itemId, SourceDocumentDetailType, SourceDocumentDetailId);
        }

        public StockMutation CreateObject(StockMutation stockMutation)
        {
            return (_validator.ValidCreateObject(stockMutation) ? _sm.CreateObject(stockMutation) : stockMutation);
        }

        public StockMutation UpdateObject(StockMutation stockMutation)
        {
            return _sm.UpdateObject(stockMutation);
        }

        public StockMutation SoftDeleteObject(StockMutation stockMutation)
        {
            return (_validator.ValidDeleteObject(stockMutation) ? _sm.SoftDeleteObject(stockMutation) : stockMutation);
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

        public IList<StockMutation> SoftDeleteStockMutationForPurchaseOrder(PurchaseOrderDetail pod, Item item)
        {
            IList<StockMutation> stockMutations = _sm.GetObjectsBySourceDocumentDetail(item.Id, Constant.SourceDocumentDetailType.PurchaseOrderDetail, pod.Id);
            foreach (var sm in stockMutations)
            {
                _sm.SoftDeleteObject(sm);
            }
            return stockMutations;
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

        public IList<StockMutation> SoftDeleteStockMutationForPurchaseReceival(PurchaseReceivalDetail prd, Item item)
        {
            IList<StockMutation> stockMutations = _sm.GetObjectsBySourceDocumentDetail(item.Id, Constant.SourceDocumentDetailType.PurchaseReceivalDetail, prd.Id);
            foreach (var sm in stockMutations)
            {
                _sm.SoftDeleteObject(sm);
            }
            return stockMutations;
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

        public IList<StockMutation> SoftDeleteStockMutationForSalesOrder(SalesOrderDetail sod, Item item)
        {
            IList<StockMutation> stockMutations = _sm.GetObjectsBySourceDocumentDetail(item.Id, Constant.SourceDocumentDetailType.SalesOrderDetail, sod.Id);
            foreach (var sm in stockMutations)
            {
                _sm.SoftDeleteObject(sm);
            }
            return stockMutations;
        }

        public IList<StockMutation> CreateStockMutationForDeliveryOrder(DeliveryOrderDetail dod, Item item)
        {
            IList<StockMutation> result = new List<StockMutation>();

            StockMutation sm = new StockMutation();
            sm.ItemId = dod.ItemId;
            sm.Quantity = dod.Quantity;
            sm.SourceDocumentType = Constant.SourceDocumentType.DeliveryOrder;
            sm.SourceDocumentId = dod.DeliveryOrderId;
            sm.SourceDocumentDetailType = Constant.SourceDocumentDetailType.DeliveryOrderDetail;
            sm.SourceDocumentDetailId = dod.Id;
            sm.ItemCase = Constant.StockMutationItemCase.PendingDelivery;
            sm.Status = Constant.StockMutationStatus.Deduction;
            result.Add(_sm.CreateObject(sm));

            sm.ItemCase = Constant.StockMutationItemCase.Ready;
            sm.Status = Constant.StockMutationStatus.Deduction;
            result.Add(_sm.CreateObject(sm));
            return result;
        }

        public IList<StockMutation> SoftDeleteStockMutationForDeliveryOrder(DeliveryOrderDetail dod, Item item)
        {
            IList<StockMutation> stockMutations = _sm.GetObjectsBySourceDocumentDetail(item.Id, Constant.SourceDocumentDetailType.DeliveryOrderDetail, dod.Id);
            foreach (var sm in stockMutations)
            {
                _sm.SoftDeleteObject(sm);
            }
            return stockMutations;
        }

    }
}
