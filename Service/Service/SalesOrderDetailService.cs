using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private ISalesOrderDetailRepository _sd;
        public SalesOrderDetailService(ISalesOrderDetailRepository _salesOrderDetailRepository)
        {
            _sd = _salesOrderDetailRepository;
        }

        public IList<SalesOrderDetail> GetObjectsBySalesOrderId(int salesOrderId)
        {
            return _sd.GetObjectsBySalesOrderId(salesOrderId);
        }

        public SalesOrderDetail GetObjectById(int Id)
        {
            return _sd.GetObjectById(Id);
        }

        public SalesOrderDetail CreateObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.CreateObject(salesOrderDetail);
        }

        public SalesOrderDetail CreateObject(int salesOrderId, int itemId, int quantity)
        {
            SalesOrderDetail sod = new SalesOrderDetail
            {
                SalesOrderId = salesOrderId,
                ItemId = itemId,
                Quantity = quantity
            };
            return _sd.CreateObject(sod);
        }

        public SalesOrderDetail UpdateObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.UpdateObject(salesOrderDetail);
        }

        public SalesOrderDetail SoftDeleteObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.SoftDeleteObject(salesOrderDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _sd.DeleteObject(Id);
        }

        public SalesOrderDetail ConfirmObject(SalesOrderDetail salesOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            salesOrderDetail = _sd.ConfirmObject(salesOrderDetail);
            Item item = _itemService.GetObjectById(salesOrderDetail.ItemId);
            item.PendingDelivery += salesOrderDetail.Quantity;
            _itemService.UpdateObject(item);
            StockMutation sm = _stockMutationService.CreateStockMutationForSalesOrder(salesOrderDetail, item);
            return salesOrderDetail;
        }

        public SalesOrderDetail UnconfirmObject(SalesOrderDetail salesOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            salesOrderDetail = _sd.UnconfirmObject(salesOrderDetail);
            Item item = _itemService.GetObjectById(salesOrderDetail.ItemId);
            item.PendingDelivery -= salesOrderDetail.Quantity;
            _itemService.UpdateObject(item);
            IList<StockMutation> sm = _stockMutationService.SoftDeleteStockMutationForSalesOrder(salesOrderDetail, item);
            return _sd.UnconfirmObject(salesOrderDetail);
        }

        public SalesOrderDetail FulfilObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.FulfilObject(salesOrderDetail);
        }
    }
}