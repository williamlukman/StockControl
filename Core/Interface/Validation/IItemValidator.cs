using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IItemValidator
    {
        Item VSku(Item i, IItemService _is);
        Item VName(Item i);
        Item VCreateObject(Item i, IItemService _is);
        Item VUpdateObject(Item i, IItemService _is);
        Item VDeleteObject(Item i, IStockMutationService _sm);
        bool ValidCreateObject(Item i, IItemService _is);
        bool ValidUpdateObject(Item i, IItemService _is);
        bool ValidDeleteObject(Item i, IStockMutationService _sm);
        bool isValid(Item i);
        string PrintError(Item i);
    }
}
