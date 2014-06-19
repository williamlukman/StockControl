using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IPurchaseReceivalValidator
    {
        PurchaseReceival VContact(PurchaseReceival pr, IContactService _cs);
        PurchaseReceival VReceivalDate(PurchaseReceival pr);
        PurchaseReceival VIsConfirmed(PurchaseReceival pr);
        PurchaseReceival VHasPurchaseReceivalDetails(PurchaseReceival pr, IPurchaseReceivalDetailService _prds);
        PurchaseReceival VCreateObject(PurchaseReceival pr, IContactService _cs);
        PurchaseReceival VUpdateObject(PurchaseReceival pr, IContactService _cs);
        PurchaseReceival VDeleteObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds);
        PurchaseReceival VConfirmObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds);
        PurchaseReceival VUnconfirmObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds, IItemService _is);
        bool ValidCreateObject(PurchaseReceival pr, IContactService _cs);
        bool ValidUpdateObject(PurchaseReceival pr, IContactService _cs);
        bool ValidDeleteObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds);
        bool ValidConfirmObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds);
        bool ValidUnconfirmObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds, IItemService _is);
        bool isValid(PurchaseReceival pr);
        string PrintError(PurchaseReceival pr);
    }
}
