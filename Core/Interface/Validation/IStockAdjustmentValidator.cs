using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IStockAdjustmentValidator
    {
        StockAdjustment VAdjustmentDate(StockAdjustment stockAdjustment);
        StockAdjustment VIsConfirmed(StockAdjustment stockAdjustment);
        StockAdjustment VHasStockAdjustmentDetails(StockAdjustment sa, IStockAdjustmentDetailService _sads);
        StockAdjustment VCreateObject(StockAdjustment stockAdjustment);
        StockAdjustment VUpdateObject(StockAdjustment stockAdjustment);
        StockAdjustment VDeleteObject(StockAdjustment stockAdjustment);
        StockAdjustment VConfirmObject(StockAdjustment stockAdjustment, IStockAdjustmentDetailService _stockAdjustmentDetailService, IItemService _itemService);
        StockAdjustment VUnconfirmObject(StockAdjustment stockAdjustment, IStockAdjustmentDetailService _stockAdjustmentDetailService, IItemService _itemService);
        bool ValidCreateObject(StockAdjustment stockAdjustment);
        bool ValidUpdateObject(StockAdjustment stockAdjustment);
        bool ValidDeleteObject(StockAdjustment stockAdjustment);
        bool ValidConfirmObject(StockAdjustment stockAdjustment, IStockAdjustmentDetailService _stockAdjustmentDetailService, IItemService _itemService);
        bool ValidUnconfirmObject(StockAdjustment stockAdjustment, IStockAdjustmentDetailService _stockAdjustmentDetailService, IItemService _itemService);
        bool isValid(StockAdjustment stockAdjustment);
        string PrintError(StockAdjustment stockAdjustment);
    }
}
