using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IStockAdjustmentDetailValidator
    {
        StockAdjustmentDetail VHasStockAdjustment(StockAdjustmentDetail sad, IStockAdjustmentService _prs);
        StockAdjustmentDetail VHasItem(StockAdjustmentDetail sad, IItemService _is);
        StockAdjustmentDetail VQuantity(StockAdjustmentDetail sad);
        StockAdjustmentDetail VPrice(StockAdjustmentDetail sad);
        StockAdjustmentDetail VUniqueItem(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IItemService _is);
        StockAdjustmentDetail VIsConfirmed(StockAdjustmentDetail sad);
        StockAdjustmentDetail VQuantityConfirm(StockAdjustmentDetail sad, IItemService _is);
        StockAdjustmentDetail VCreateObject(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IStockAdjustmentService _prs, IItemService _is);
        StockAdjustmentDetail VUpdateObject(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IStockAdjustmentService _prs, IItemService _is);
        StockAdjustmentDetail VDeleteObject(StockAdjustmentDetail sad);
        StockAdjustmentDetail VConfirmObject(StockAdjustmentDetail sad, IItemService _is);
        StockAdjustmentDetail VUnconfirmObject(StockAdjustmentDetail sad, IItemService _is);
        bool ValidCreateObject(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IStockAdjustmentService _prs, IItemService _is);
        bool ValidUpdateObject(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IStockAdjustmentService _prs,  IItemService _is);
        bool ValidDeleteObject(StockAdjustmentDetail sad);
        bool ValidConfirmObject(StockAdjustmentDetail sad, IItemService _is);
        bool ValidUnconfirmObject(StockAdjustmentDetail sad, IItemService _is);
        bool isValid(StockAdjustmentDetail sad);
        string PrintError(StockAdjustmentDetail sad);
    }
}
