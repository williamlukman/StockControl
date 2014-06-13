using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class StockAdjustmentService : IStockAdjustmentService
    {
        private IStockAdjustmentRepository _repository;
        private IStockAdjustmentValidator _validator;

        public StockAdjustmentService(IStockAdjustmentRepository _stockAdjustmentRepository, IStockAdjustmentValidator _stockAdjustmentValidator)
        {
            _repository = _stockAdjustmentRepository;
            _validator = _stockAdjustmentValidator;
        }

        public IStockAdjustmentValidator GetValidator()
        {
            return _validator;
        }

        public IList<StockAdjustment> GetAll()
        {
            return _repository.GetAll();
        }

        public StockAdjustment GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }
        
        public StockAdjustment CreateObject(StockAdjustment stockAdjustment)
        {
            stockAdjustment.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(stockAdjustment) ? _repository.CreateObject(stockAdjustment) : stockAdjustment);
        }

        public StockAdjustment CreateObject(DateTime adjustmentDate)
        {
            StockAdjustment sa = new StockAdjustment
            {
                AdjustmentDate = adjustmentDate
            };
            return this.CreateObject(sa);
        }

        public StockAdjustment UpdateObject(StockAdjustment stockAdjustment)
        {
            stockAdjustment.Errors.Clear();
            return (_validator.ValidUpdateObject(stockAdjustment) ? _repository.UpdateObject(stockAdjustment) : stockAdjustment);
        }

        public StockAdjustment SoftDeleteObject(StockAdjustment stockAdjustment, IStockAdjustmentDetailService _stockAdjustmentDetailService)
        {
            stockAdjustment.Errors.Clear();
            return (_validator.ValidDeleteObject(stockAdjustment) ? _repository.SoftDeleteObject(stockAdjustment) : stockAdjustment);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public StockAdjustment ConfirmObject(StockAdjustment stockAdjustment, IStockAdjustmentDetailService _stockAdjustmentDetailService, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            stockAdjustment.Errors.Clear();
            if (_validator.ValidConfirmObject(stockAdjustment, _stockAdjustmentDetailService, _itemService))
            {
                _repository.ConfirmObject(stockAdjustment);
                IList<StockAdjustmentDetail> details = _stockAdjustmentDetailService.GetObjectsByStockAdjustmentId(stockAdjustment.Id);
                foreach (var detail in details)
                {
                    detail.ConfirmedAt = stockAdjustment.ConfirmedAt;
                    _stockAdjustmentDetailService.ConfirmObject(detail, _stockMutationService, _itemService);
                }
            }
            return stockAdjustment;
        }

        public StockAdjustment UnconfirmObject(StockAdjustment stockAdjustment, IStockAdjustmentDetailService _stockAdjustmentDetailService,
                                               IStockMutationService _stockMutationService, IItemService _itemService)
        {
            stockAdjustment.Errors.Clear();
            if (_validator.ValidUnconfirmObject(stockAdjustment, _stockAdjustmentDetailService, _itemService))
            {
                _repository.UnconfirmObject(stockAdjustment);
                IList<StockAdjustmentDetail> details = _stockAdjustmentDetailService.GetObjectsByStockAdjustmentId(stockAdjustment.Id);
                foreach (var detail in details)
                {
                    _stockAdjustmentDetailService.UnconfirmObject(detail, _stockMutationService, _itemService);
                }
            }
            return stockAdjustment;
        }
    }
}