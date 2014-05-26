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
                i.Errors.Add("Error. Empty or Invalid Name");
            }
            if (_is.IsSkuDuplicated(i.Sku))
            {
                i.Errors.Add("Error. Duplicate Sku");
            }

            return i;
        }

        public Item VName(Item i)
        {
            if (String.IsNullOrEmpty(i.Name) || i.Name.Trim() == "")
            {
                i.Errors.Add("Error. Empty or Invalid Name");
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
                i.Errors.Add("Error. Item has associated stock mutations");
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
            VUpdateObject(i, _is);
            return isValid(i);
        }

        public bool ValidDeleteObject(Item i, IStockMutationService _sm)
        {
            VDeleteObject(i, _sm);
            return isValid(i);
        }

        public bool isValid(Item i)
        {
            bool isValid = !i.Errors.Any();
            return isValid;
        }

        public string PrintError(Item i)
        {
            string erroroutput = "";
            foreach (var item in i.Errors)
            {
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}
