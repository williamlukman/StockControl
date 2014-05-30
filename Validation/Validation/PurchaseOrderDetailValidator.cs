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
    public class PurchaseOrderDetailValidator : IPurchaseOrderDetailValidator
    {
        public PurchaseOrderDetail VHasPurchaseOrder(PurchaseOrderDetail pod, IPurchaseOrderService _pos)
        {
            PurchaseOrder po = _pos.GetObjectById(pod.PurchaseOrderId);
            if (po == null)
            {
                pod.Errors.Add("Error. Purchase Order does not exist");
            }
            return pod;
        }

        public PurchaseOrderDetail VHasItem(PurchaseOrderDetail pod, IItemService _is)
        {
            Item item = _is.GetObjectById(pod.ItemId);
            if (item == null)
            {
                pod.Errors.Add("Error. Item does not exist");
            }
            return pod;
        }

        public PurchaseOrderDetail VQuantity(PurchaseOrderDetail pod)
        {
            if (pod.Quantity < 0)
            {
                pod.Errors.Add("Error. Quantity must be greater than or equal to zero");
            }
            return pod;
        }

        public PurchaseOrderDetail VPrice(PurchaseOrderDetail pod)
        {
            if (pod.Price <= 0)
            {
                pod.Errors.Add("Error. Price must be greater than zero");
            }
            return pod;
        }

        public PurchaseOrderDetail VUniquePOD(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IItemService _is)
        {
            IList<PurchaseOrderDetail> details = _pods.GetObjectsByPurchaseOrderId(pod.PurchaseOrderId);
            foreach (var detail in details)
            {
                if (detail.ItemId == pod.ItemId)
                {
                    pod.Errors.Add("Error. Purchase order detail is not unique in this purchase order");
                    return pod;
                }
            }
            return pod;
        }

        public PurchaseOrderDetail VIsConfirmed(PurchaseOrderDetail pod)
        {
            if (pod.IsConfirmed)
            {
                pod.Errors.Add("Error. Purchase order detail is already confirmed");
            }
            return pod;
        }

        public PurchaseOrderDetail VHasItemPendingReceival(PurchaseOrderDetail pod, IItemService _is)
        {
            Item item = _is.GetObjectById(pod.ItemId);
            if (item.PendingReceival < pod.Quantity)
            {
                pod.Errors.Add("Error. Current item pending receival is less than the quantity of purchase order detail");
            }
            return pod;
        }

        public PurchaseOrderDetail VConfirmedPurchaseReceival(PurchaseOrderDetail pod, IPurchaseReceivalDetailService _prds)
        {
            PurchaseReceivalDetail prd = _prds.GetObjectByPurchaseOrderDetailId(pod.Id);
            if (prd == null)
            {
                return pod;
            }
            if (prd.IsConfirmed)
            {
                pod.Errors.Add("Error. Associated purchase receival is confirmed already");
            }
            return pod;
        }

        public PurchaseOrderDetail VCreateObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is)
        {

            VHasPurchaseOrder(pod, _pos);
            VHasItem(pod, _is);
            VQuantity(pod);
            VPrice(pod);
            VUniquePOD(pod, _pods, _is);
            return pod;
        }

        public PurchaseOrderDetail VUpdateObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is)
        {
            VHasPurchaseOrder(pod, _pos);
            VHasItem(pod, _is);
            VQuantity(pod);
            VPrice(pod);
            VUniquePOD(pod, _pods, _is);
            VIsConfirmed(pod);
            return pod;
        }

        public PurchaseOrderDetail VDeleteObject(PurchaseOrderDetail pod)
        {
            VIsConfirmed(pod);
            return pod;
        }

        public PurchaseOrderDetail VConfirmObject(PurchaseOrderDetail pod)
        {
            VIsConfirmed(pod);
            return pod;
        }

        public PurchaseOrderDetail VUnconfirmObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseReceivalDetailService _prds, IItemService _is)
        {
            VHasItemPendingReceival(pod, _is);
            VConfirmedPurchaseReceival(pod, _prds);
            return pod;
        }

        public bool ValidCreateObject(PurchaseOrderDetail pod,  IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is)
        {
            VCreateObject(pod, _pods, _pos, _is);
            return isValid(pod);
        }

        public bool ValidUpdateObject(PurchaseOrderDetail pod,  IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is)
        {
            VUpdateObject(pod, _pods, _pos, _is);
            return isValid(pod);
        }

        public bool ValidDeleteObject(PurchaseOrderDetail pod)
        {
            VDeleteObject(pod);
            return isValid(pod);
        }

        public bool ValidConfirmObject(PurchaseOrderDetail pod)
        {
            VConfirmObject(pod);
            return isValid(pod);
        }

        public bool ValidUnconfirmObject(PurchaseOrderDetail pod, IPurchaseOrderDetailService _pods, IPurchaseReceivalDetailService _prds, IItemService _is)
        {
            VUnconfirmObject(pod, _pods, _prds, _is);
            return isValid(pod);
        }

        public bool isValid(PurchaseOrderDetail pod)
        {
            bool isValid = !pod.Errors.Any();
            return isValid;
        }

        public string PrintError(PurchaseOrderDetail pod)
        {
            string erroroutput = pod.Errors.ElementAt(0);
            foreach (var item in pod.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}