using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IPurchaseOrderDetailValidator
    {
        PurchaseOrderDetail VHasPurchaseOrder(PurchaseOrderDetail pod, IPurchaseOrderService _pos);
        PurchaseOrderDetail VHasItem(PurchaseOrderDetail pod, IItemService _is);
        PurchaseOrderDetail VQuantity(PurchaseOrderDetail pod);
        PurchaseOrderDetail VUniquePOD(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IItemService _is);
        PurchaseOrderDetail VIsConfirmed(PurchaseOrderDetail pod);
        PurchaseOrderDetail VHasItemPendingReceival(PurchaseOrderDetail pod, IItemService _is);
        PurchaseOrderDetail VConfirmedPurchaseReceival(PurchaseOrderDetail pod, IPurchaseReceivalDetailService _prds);
        PurchaseOrderDetail VCreateObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is);
        PurchaseOrderDetail VUpdateObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is);
        PurchaseOrderDetail VDeleteObject(PurchaseOrderDetail pod);
        PurchaseOrderDetail VConfirmObject(PurchaseOrderDetail pod);
        PurchaseOrderDetail VUnconfirmObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseReceivalDetailService _prds, IItemService _is);
        bool ValidCreateObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is);
        bool ValidUpdateObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is);
        bool ValidDeleteObject(PurchaseOrderDetail pod);
        bool ValidConfirmObject(PurchaseOrderDetail pod);
        bool ValidUnconfirmObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseReceivalDetailService _prds, IItemService _is);
        bool isValid(PurchaseOrderDetail pod);
        string PrintError(PurchaseOrderDetail pod);
    }
}
