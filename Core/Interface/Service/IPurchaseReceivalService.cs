using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseReceivalService
    {
        IPurchaseReceivalValidator GetValidator();
        IList<PurchaseReceival> GetAll();
        PurchaseReceival GetObjectById(int Id);
        IList<PurchaseReceival> GetObjectsByContactId(int contactId);
        PurchaseReceival CreateObject(PurchaseReceival purchaseReceival, IContactService _contactService);
        PurchaseReceival CreateObject(int contactId, DateTime ReceivalDate, IContactService _contactService);
        PurchaseReceival UpdateObject(PurchaseReceival purchaseReceival, IContactService _contactService);
        PurchaseReceival SoftDeleteObject(PurchaseReceival purchaseReceival, IPurchaseReceivalDetailService _purchaseReceivalDetailService);
        bool DeleteObject(int Id);
        PurchaseReceival ConfirmObject(PurchaseReceival purchaseReceival, IPurchaseReceivalDetailService _prds,
                                    IPurchaseOrderDetailService _pods, IStockMutationService _stockMutationService, IItemService _itemService);
        PurchaseReceival UnconfirmObject(PurchaseReceival purchaseReceival, IPurchaseReceivalDetailService _prds,
                                    IStockMutationService _stockMutationService, IItemService _itemService);
    }
}