using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;

namespace Validation.Validation
{
    public class ItemValidator : IItemValidator
    {
        public Item VSku(Item i, IItemService _is)
        {
            if (String.IsNullOrEmpty(i.Sku) || i.Sku.Trim() == "")
            {
                i.Errors.Add("Sku", "Tidak boleh kosong");
            }
            if (_is.IsSkuDuplicated(i))
            {
                i.Errors.Add("Sku", "Tidak boleh ada duplikasi");
            }

            return i;
        }

        public Item VName(Item i)
        {
            if (String.IsNullOrEmpty(i.Name) || i.Name.Trim() == "")
            {
                i.Errors.Add("Name", "Tidak boleh kosong");
            }
            return i;

        }
        public Item VCreateObject(Item i, IItemService _is)
        {
            VSku(i, _is);
            VName(i);
            return i;
        }

        public Item VUpdateObject(Item i, IItemService _is)
        {
            VSku(i, _is);
            VName(i);
            return i;
        }

        public Item VDeleteObject(Item i, IStockMutationService _sm)
        {
            IList<StockMutation> stockMutations = _sm.GetObjectsByItemId(i.Id);
            if (stockMutations.Any())
            {
                i.Errors.Add("Item", "Tidak boleh ada asosiasi dengan Stock Mutations");
            }
            return i;
        }

        public bool ValidCreateObject(Item i, IItemService _is)
        {
            VCreateObject(i, _is);
            return isValid(i);
        }

        public bool ValidUpdateObject(Item i, IItemService _is)
        {
            i.Errors.Clear();
            VUpdateObject(i, _is);
            return isValid(i);
        }

        public bool ValidDeleteObject(Item i, IStockMutationService _sm)
        {
            i.Errors.Clear();
            VDeleteObject(i, _sm);
            return isValid(i);
        }

        public bool isValid(Item obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(Item obj)
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
