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
                sa.Errors.Add("AdjustmentDate", "Tidak boleh tidak ada");
            }
            */
            return sa;
        }

        public StockAdjustment VIsConfirmed(StockAdjustment sa)
        {
            if (sa.IsConfirmed)
            {
                sa.Errors.Add("IsConfirmed", "Tidak boleh sudah dikonfirmasi");
            }
            return sa;
        }

        public StockAdjustment VHasStockAdjustmentDetails(StockAdjustment sa, IStockAdjustmentDetailService _sads)
        {
            IList<StockAdjustmentDetail> details = _sads.GetObjectsByStockAdjustmentId(sa.Id);
            if (!details.Any())
            {
                sa.Errors.Add("StockAdjustmentDetails", "Tidak boleh tidak ada");
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
                    _sads.GetValidator().ValidConfirmObject(detail, _is);
                    foreach (var error in detail.Errors)
                    {
                        sa.Errors.Add(error.Key, error.Value);
                    }
                    if (sa.Errors.Any()) { return sa; }
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
                    _sads.GetValidator().ValidUnconfirmObject(detail, _is);
                    foreach (var error in detail.Errors)
                    {
                        sa.Errors.Add(error.Key, error.Value);
                    }
                    if (sa.Errors.Any()) { return sa; }
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
            sa.Errors.Clear();
            VUpdateObject(sa);
            return isValid(sa);
        }

        public bool ValidDeleteObject(StockAdjustment sa)
        {
            sa.Errors.Clear();
            VDeleteObject(sa);
            return isValid(sa);
        }

        public bool ValidConfirmObject(StockAdjustment sa, IStockAdjustmentDetailService _sads, IItemService _is)
        {
            sa.Errors.Clear();
            VConfirmObject(sa, _sads, _is);
            return isValid(sa);
        }

        public bool ValidUnconfirmObject(StockAdjustment sa, IStockAdjustmentDetailService _sads, IItemService _is)
        {
            sa.Errors.Clear();
            VUnconfirmObject(sa, _sads, _is);
            return isValid(sa);
        }

        public bool isValid(StockAdjustment obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(StockAdjustment obj)
        {
            string erroroutput = "";
            KeyValuePair<string, string> first = obj.Errors.ElementAt(0);
            erroroutput += first.Key + "," + first.Value;
            foreach (KeyValuePair<string, string> pair in obj.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += pair.Key + "," + pair.Value;
            }
            return erroroutput;
        }

    }
}