using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IPurchaseOrderValidator
    {
        PurchaseOrder VCustomer(PurchaseOrder po, IContactService _cs);
        PurchaseOrder VPurchaseDate(PurchaseOrder po);
        PurchaseOrder VIsConfirmed(PurchaseOrder po);
        PurchaseOrder VHasPurchaseOrderDetails(PurchaseOrder po, IPurchaseOrderDetailService _pods);
        PurchaseOrder VHasItemPendingReceival(PurchaseOrder po, IPurchaseOrderDetailService _pods, IItemService _is);
        PurchaseOrder VCreateObject(PurchaseOrder po, IContactService _cs);
        PurchaseOrder VUpdateObject(PurchaseOrder po, IContactService _cs);
        PurchaseOrder VDeleteObject(PurchaseOrder po);
        PurchaseOrder VConfirmObject(PurchaseOrder po, IPurchaseOrderDetailService _pods);
        PurchaseOrder VUnconfirmObject(PurchaseOrder po, IPurchaseOrderDetailService _pods, IItemService _is);
        bool ValidCreateObject(PurchaseOrder po, IContactService _cs);
        bool ValidUpdateObject(PurchaseOrder po, IContactService _cs);
        bool ValidDeleteObject(PurchaseOrder po);
        bool ValidConfirmObject(PurchaseOrder po, IPurchaseOrderDetailService _pods);
        bool ValidUnconfirmObject(PurchaseOrder po, IPurchaseOrderDetailService _pods, IItemService _is);
        bool isValid(PurchaseOrder po);
        string PrintError(PurchaseOrder po);
    }
}
