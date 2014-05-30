using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;
using Core.Constant;

namespace Validation.Validation
{
    public class StockAdjustmentDetailValidator : IStockAdjustmentDetailValidator
    {
        public StockAdjustmentDetail VHasStockAdjustment(StockAdjustmentDetail sad, IStockAdjustmentService _sas)
        {
            StockAdjustment sa = _sas.GetObjectById(sad.StockAdjustmentId);
            if (sa == null)
            {
                sad.Errors.Add("Error. Stock adjustment does not exist");
            }
            return sad;
        }

        public StockAdjustmentDetail VHasItem(StockAdjustmentDetail sad, IItemService _is)
        {
            Item item = _is.GetObjectById(sad.ItemId);
            if (item == null)
            {
                sad.Errors.Add("Error. Item does not exist");
            }
            return sad;
        }

        public StockAdjustmentDetail VQuantity(StockAdjustmentDetail sad)
        {
            if (sad.Quantity == 0)
            {
                sad.Errors.Add("Error. Quantity can be negative but must not be zero");
            }
            return sad;
        }

        public StockAdjustmentDetail VPrice(StockAdjustmentDetail sad)
        {
            if (sad.Quantity <= 0)
            {
                sad.Errors.Add("Error. Price must be greater than zero");
            }
            return sad;
        }

        public StockAdjustmentDetail VUniqueItem(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IItemService _is)
        {
            IList<StockAdjustmentDetail> details = _sads.GetObjectsByStockAdjustmentId(sad.StockAdjustmentId);
            foreach (var detail in details)
            {
                if (detail.ItemId == sad.ItemId && detail.Id != sad.Id)
                {
                     sad.Errors.Add("Error. duplicate item is not allowed in one stock adjustment");
                }
            }
            return sad;
        }

        public StockAdjustmentDetail VIsConfirmed(StockAdjustmentDetail sad)
        {
            if (sad.IsConfirmed)
            {
                sad.Errors.Add("Error. Stock adjustment detail is already confirmed");
            }
            return sad;
        }

        public StockAdjustmentDetail VQuantityConfirm(StockAdjustmentDetail sad, IItemService _is)
        {
            Item item = _is.GetObjectById(sad.ItemId);
            if (item.Ready + sad.Quantity < 0)
            {
                sad.Errors.Add("Error. Quantity of item and stock adjustment detail cannot amount to a number less than zero");
            }
            if (_is.CalculateAvgCost(item, sad.Quantity, sad.Price) < 0)
            {
                sad.Errors.Add("Error. New average cost cannot amount less than 0");
            }
            return sad;
        }

        public StockAdjustmentDetail VQuantityUnconfirm(StockAdjustmentDetail sad, IItemService _is)
        {
            Item item = _is.GetObjectById(sad.ItemId);
            if (item.Ready - sad.Quantity < 0)
            {
                sad.Errors.Add("Error. Quantity of item and stock adjustment detail cannot amount to a number less than zero");
            }
            if (_is.CalculateAvgCost(item, sad.Quantity * (-1), sad.Price) < 0)
            {
                sad.Errors.Add("Error. New average cost cannot amount less than 0");
            }
            return sad;
        }

        public StockAdjustmentDetail VCreateObject(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IStockAdjustmentService _sas, IItemService _is)
        {
            VHasStockAdjustment(sad, _sas);
            VHasItem(sad, _is);
            VQuantity(sad);
            VPrice(sad);
            VUniqueItem(sad, _sads, _is);
            return sad;
        }

        public StockAdjustmentDetail VUpdateObject(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IStockAdjustmentService _sas, IItemService _is)
        {
            VHasStockAdjustment(sad, _sas);
            VHasItem(sad, _is);
            VQuantity(sad);
            VPrice(sad);
            VUniqueItem(sad, _sads, _is);
            VIsConfirmed(sad);
            return sad;
        }

        public StockAdjustmentDetail VDeleteObject(StockAdjustmentDetail sad)
        {
            VIsConfirmed(sad);
            return sad;
        }

        public StockAdjustmentDetail VConfirmObject(StockAdjustmentDetail sad, IItemService _is)
        {
            VIsConfirmed(sad);
            VQuantityConfirm(sad, _is);
            return sad;
        }

        public StockAdjustmentDetail VUnconfirmObject(StockAdjustmentDetail sad, IItemService _is)
        {
            VQuantityUnconfirm(sad, _is);
            return sad;
        }

        public bool ValidCreateObject(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IStockAdjustmentService _sas, IItemService _is)
        {
            VCreateObject(sad, _sads, _sas, _is);
            return isValid(sad);
        }

        public bool ValidUpdateObject(StockAdjustmentDetail sad, IStockAdjustmentDetailService _sads, IStockAdjustmentService _sas, IItemService _is)
        {
            VUpdateObject(sad, _sads, _sas, _is);
            return isValid(sad);
        }

        public bool ValidDeleteObject(StockAdjustmentDetail sad)
        {
            VDeleteObject(sad);
            return isValid(sad);
        }

        public bool ValidConfirmObject(StockAdjustmentDetail sad, IItemService _is)
        {
            VConfirmObject(sad, _is);
            return isValid(sad);
        }

        public bool ValidUnconfirmObject(StockAdjustmentDetail sad, IItemService _is)
        {
            VUnconfirmObject(sad, _is);
            return isValid(sad);
        }

        public bool isValid(StockAdjustmentDetail sad)
        {
            bool isValid = !sad.Errors.Any();
            return isValid;
        }

        public string PrintError(StockAdjustmentDetail sad)
        {
            string erroroutput = sad.Errors.ElementAt(0);
            foreach (var item in sad.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}