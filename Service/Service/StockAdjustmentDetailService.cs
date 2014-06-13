using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class StockAdjustmentDetailService : IStockAdjustmentDetailService
    {
        private IStockAdjustmentDetailRepository _repository;
        private IStockAdjustmentDetailValidator _validator;

        public StockAdjustmentDetailService(IStockAdjustmentDetailRepository _stockAdjustmentDetailRepository, IStockAdjustmentDetailValidator _stockAdjustmentDetailValidator)
        {
            _repository = _stockAdjustmentDetailRepository;
            _validator = _stockAdjustmentDetailValidator;
        }

        public IStockAdjustmentDetailValidator GetValidator()
        {
            return _validator;
        }

        public IList<StockAdjustmentDetail> GetObjectsByStockAdjustmentId(int stockAdjustmentId)
        {
            return _repository.GetObjectsByStockAdjustmentId(stockAdjustmentId);
        }

        public StockAdjustmentDetail GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public StockAdjustmentDetail CreateObject(StockAdjustmentDetail stockAdjustmentDetail, IStockAdjustmentService _stockAdjustmentService, IItemService _itemService)
        {
            stockAdjustmentDetail.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(stockAdjustmentDetail, this, _stockAdjustmentService, _itemService) ?
                                        _repository.CreateObject(stockAdjustmentDetail) : stockAdjustmentDetail);
        }

        public StockAdjustmentDetail CreateObject(int stockAdjustmentId, int itemId, int quantity, decimal price,
                                                    IStockAdjustmentService _stockAdjustmentService, IItemService _itemService)
        {
            StockAdjustmentDetail sad = new StockAdjustmentDetail
            {
                StockAdjustmentId = stockAdjustmentId,
                ItemId = itemId,
                Quantity = quantity,
                Price = price
            };
            return this.CreateObject(sad, _stockAdjustmentService, _itemService);
        }


        public StockAdjustmentDetail UpdateObject(StockAdjustmentDetail stockAdjustmentDetail, IStockAdjustmentService _stockAdjustmentService, IItemService _itemService)
        {
            stockAdjustmentDetail.Errors.Clear();
            return (_validator.ValidUpdateObject(stockAdjustmentDetail, this, _stockAdjustmentService, _itemService) ?
                    _repository.UpdateObject(stockAdjustmentDetail) : stockAdjustmentDetail);
        }

        public StockAdjustmentDetail SoftDeleteObject(StockAdjustmentDetail stockAdjustmentDetail)
        {
            stockAdjustmentDetail.Errors.Clear();
            return (_validator.ValidDeleteObject(stockAdjustmentDetail) ? _repository.SoftDeleteObject(stockAdjustmentDetail) : stockAdjustmentDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public StockAdjustmentDetail ConfirmObject(StockAdjustmentDetail stockAdjustmentDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            stockAdjustmentDetail.Errors.Clear();
            if (_validator.ValidConfirmObject(stockAdjustmentDetail, _itemService))
            {
                stockAdjustmentDetail = _repository.ConfirmObject(stockAdjustmentDetail);
                Item item = _itemService.GetObjectById(stockAdjustmentDetail.ItemId);
                item.AvgCost = _itemService.CalculateAvgCost(item, stockAdjustmentDetail.Quantity, stockAdjustmentDetail.Price);
                item.Ready += stockAdjustmentDetail.Quantity;
                _itemService.UpdateObject(item);
                StockMutation sm = _stockMutationService.CreateStockMutationForStockAdjustment(stockAdjustmentDetail, item);
            }
            return stockAdjustmentDetail;
        }

        public StockAdjustmentDetail UnconfirmObject(StockAdjustmentDetail stockAdjustmentDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            stockAdjustmentDetail.Errors.Clear();
            if (_validator.ValidUnconfirmObject(stockAdjustmentDetail, _itemService))
            {
                stockAdjustmentDetail = _repository.UnconfirmObject(stockAdjustmentDetail);
                Item item = _itemService.GetObjectById(stockAdjustmentDetail.ItemId);
                item.AvgCost = _itemService.CalculateAvgCost(item, stockAdjustmentDetail.Quantity * (-1), stockAdjustmentDetail.Price);
                item.Ready -= stockAdjustmentDetail.Quantity;
                _itemService.UpdateObject(item);
                IList<StockMutation> sm = _stockMutationService.SoftDeleteStockMutationForStockAdjustment(stockAdjustmentDetail, item);
            }
            return stockAdjustmentDetail;
        }
    }
}