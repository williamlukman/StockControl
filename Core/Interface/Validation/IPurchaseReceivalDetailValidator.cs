using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IPurchaseReceivalDetailValidator
    {
        PurchaseReceivalDetail VHasPurchaseReceival(PurchaseReceivalDetail prd, IPurchaseReceivalService _prs);
        PurchaseReceivalDetail VHasItem(PurchaseReceivalDetail prd, IItemService _is);
        PurchaseReceivalDetail VContact(PurchaseReceivalDetail prd, IPurchaseReceivalService _prs, IPurchaseOrderService _pos, IPurchaseOrderDetailService _pods, IContactService _cs);
        PurchaseReceivalDetail VQuantityCreate(PurchaseReceivalDetail prd, IPurchaseOrderDetailService _pods);
        PurchaseReceivalDetail VQuantityUpdate(PurchaseReceivalDetail prd, IPurchaseOrderDetailService _pods);
        PurchaseReceivalDetail VHasPurchaseOrderDetail(PurchaseReceivalDetail prd, IPurchaseOrderDetailService _pods);
        PurchaseReceivalDetail VUniquePOD(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IItemService _is);
        PurchaseReceivalDetail VIsConfirmed(PurchaseReceivalDetail prd);
        PurchaseReceivalDetail VHasItemQuantity(PurchaseReceivalDetail prd, IItemService _is);
        PurchaseReceivalDetail VCreateObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IPurchaseReceivalService _prs, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is, IContactService _cs);
        PurchaseReceivalDetail VUpdateObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IPurchaseReceivalService _prs, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is, IContactService _cs);
        PurchaseReceivalDetail VDeleteObject(PurchaseReceivalDetail prd);
        PurchaseReceivalDetail VConfirmObject(PurchaseReceivalDetail prd);
        PurchaseReceivalDetail VUnconfirmObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IItemService _is);
        bool ValidCreateObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IPurchaseReceivalService _prs, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is, IContactService _cs);
        bool ValidUpdateObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IPurchaseReceivalService _prs, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is, IContactService _cs);
        bool ValidDeleteObject(PurchaseReceivalDetail prd);
        bool ValidConfirmObject(PurchaseReceivalDetail prd);
        bool ValidUnconfirmObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IItemService _is);
        bool isValid(PurchaseReceivalDetail prd);
        string PrintError(PurchaseReceivalDetail prd);
    }
}
