using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseReceivalService
    {
        IList<PurchaseReceival> GetAll();
        PurchaseReceival GetObjectById(int Id);
        IList<PurchaseReceival> GetObjectsByContactId(int contactId);
        PurchaseReceival CreateObject(PurchaseReceival purchaseReceival);
        PurchaseReceival CreateObject(int contactId, DateTime ReceivalDate);
        PurchaseReceival UpdateObject(PurchaseReceival purchaseReceival);
        PurchaseReceival SoftDeleteObject(PurchaseReceival purchaseReceival);
        bool DeleteObject(int Id);
        PurchaseReceival ConfirmObject(PurchaseReceival purchaseReceival);
        PurchaseReceival UnconfirmObject(PurchaseReceival purchaseReceival);
    }
}