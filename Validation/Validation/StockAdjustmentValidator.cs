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
    public class StockAdjustmentValidator : IStockAdjustmentValidator
    {

        public StockAdjustment VAdjustmentDate(StockAdjustment sa)
        {
            /* adjustment is never null
            if (sa.AdjustmentDate == null)
            {
                sa.Errors.Add("Error. Adjustment Date does not exist");
            }
            */
            return sa;
        }

        public StockAdjustment VIsConfirmed(StockAdjustment sa)
        {
            if (sa.IsConfirmed)
            {
                sa.Errors.Add("Error. Stock Adjustment is confirmed already");
            }
            return sa;
        }

        public StockAdjustment VHasStockAdjustmentDetails(StockAdjustment sa, IStockAdjustmentDetailService _sads)
        {
            IList<StockAdjustmentDetail> details = _sads.GetObjectsByStockAdjustmentId(sa.Id);
            if (!details.Any())
            {
                sa.Errors.Add("Error. Stock Adjustment does not have stock adjustment details");
            }
            return sa;
        }

        public StockAdjustment VCreateObject(StockAdjustment sa)
        {
            VAdjustmentDate(sa);
            return sa;
        }

        public StockAdjustment VUpdateObject(StockAdjustment sa)
        {
            VAdjustmentDate(sa);
            VIsConfirmed(sa);
            return sa;
        }

        public StockAdjustment VDeleteObject(StockAdjustment sa)
        {
            VIsConfirmed(sa);
            return sa;
        }

        public StockAdjustment VConfirmObject(StockAdjustment sa, IStockAdjustmentDetailService _sads, IItemService _is)
        {
            VIsConfirmed(sa);
            VHasStockAdjustmentDetails(sa, _sads);
            if (isValid(sa))
            {
                IList<StockAdjustmentDetail> details = _sads.GetObjectsByStockAdjustmentId(sa.Id);
                foreach (var detail in details)
                {
                    _sads.GetValidator().VConfirmObject(detail, _is);
                    sa.Errors.UnionWith(detail.Errors);
                }
            }
            return sa;
        }

        public StockAdjustment VUnconfirmObject(StockAdjustment sa, IStockAdjustmentDetailService _sads, IItemService _is)
        {            
            if (isValid(sa))
            {
                IList<StockAdjustmentDetail> details = _sads.GetObjectsByStockAdjustmentId(sa.Id);
                foreach (var detail in details)
                {
                    _sads.GetValidator().VUnconfirmObject(detail, _is);
                    sa.Errors.UnionWith(detail.Errors);
                }
            }

            return sa;
        }

        public bool ValidCreateObject(StockAdjustment sa)
        {
            VCreateObject(sa);
            return isValid(sa);
        }

        public bool ValidUpdateObject(StockAdjustment sa)
        {
            VUpdateObject(sa);
            return isValid(sa);
        }

        public bool ValidDeleteObject(StockAdjustment sa)
        {
            VDeleteObject(sa);
            return isValid(sa);
        }

        public bool ValidConfirmObject(StockAdjustment sa, IStockAdjustmentDetailService _sads, IItemService _is)
        {
            VConfirmObject(sa, _sads, _is);
            return isValid(sa);
        }

        public bool ValidUnconfirmObject(StockAdjustment sa, IStockAdjustmentDetailService _sads, IItemService _is)
        {
            VUnconfirmObject(sa, _sads, _is);
            return isValid(sa);
        }

        public bool isValid(StockAdjustment sa)
        {
            bool isValid = !sa.Errors.Any();
            return isValid;
        }

        public string PrintError(StockAdjustment sa)
        {
            string erroroutput = sa.Errors.ElementAt(0);
            foreach (var item in sa.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}