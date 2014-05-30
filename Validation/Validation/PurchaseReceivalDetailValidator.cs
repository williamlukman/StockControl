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
    public class PurchaseReceivalDetailValidator : IPurchaseReceivalDetailValidator
    {
        public PurchaseReceivalDetail VHasPurchaseReceival(PurchaseReceivalDetail prd, IPurchaseReceivalService _prs)
        {
            PurchaseReceival pr = _prs.GetObjectById(prd.PurchaseReceivalId);
            if (pr == null)
            {
                prd.Errors.Add("Error. Purchase Receival does not exist");
            }
            return prd;
        }

        public PurchaseReceivalDetail VHasItem(PurchaseReceivalDetail prd, IItemService _is)
        {
            Item item = _is.GetObjectById(prd.ItemId);
            if (item == null)
            {
                prd.Errors.Add("Error. Item does not exist");
            }
            return prd;
        }

        public PurchaseReceivalDetail VCustomer(PurchaseReceivalDetail prd, IPurchaseReceivalService _prs, IPurchaseOrderService _pos, IPurchaseOrderDetailService _pods, IContactService _cs)
        {
            PurchaseReceival pr = _prs.GetObjectById(prd.PurchaseReceivalId);
            PurchaseOrderDetail pod = _pods.GetObjectById(prd.PurchaseOrderDetailId);
            if (pod == null)
            {
                prd.Errors.Add("Error. Could not find the associated purchase order detail");
                return prd;
            }
            PurchaseOrder po = _pos.GetObjectById(pod.PurchaseOrderId);
            if (po.CustomerId != pr.CustomerId)
            {
                prd.Errors.Add("Error. Contact does not match of purchase receival and purchase order");
            }
            return prd;
        }

        public PurchaseReceivalDetail VQuantityCreate(PurchaseReceivalDetail prd, IPurchaseOrderDetailService _pods)
        {
            PurchaseOrderDetail pod = _pods.GetObjectById(prd.PurchaseOrderDetailId);
            if (pod == null)
            {
                prd.Errors.Add("Error. Could not find the associated purchase order detail");
                return prd;
            }
            if (prd.Quantity <= 0)
            {
                prd.Errors.Add("Error. Quantity must be greater than zero");
                return prd;
            }
            if (prd.Quantity > pod.Quantity)
            {
                prd.Errors.Add("Error. Quantity must be less than purchase order's quantity of " + pod.Quantity);
                return prd;
            }
            return prd;
        }

        public PurchaseReceivalDetail VQuantityUpdate(PurchaseReceivalDetail prd, IPurchaseOrderDetailService _pods)
        {
            PurchaseOrderDetail pod = _pods.GetObjectById(prd.PurchaseOrderDetailId);
            if (prd.Quantity <= 0)
            {
                prd.Errors.Add("Error. Quantity must be greater or equal to zero");
            }
            if (prd.Quantity > pod.Quantity)
            {
                prd.Errors.Add("Error. Quantity must be less than purchase order's quantity of " + pod.Quantity);
            }
            return prd;
        }

        public PurchaseReceivalDetail VHasPurchaseOrderDetail(PurchaseReceivalDetail prd, IPurchaseOrderDetailService _pods)
        {
            PurchaseOrderDetail pod = _pods.GetObjectById(prd.PurchaseOrderDetailId);
            if (pod == null)
            {
                prd.Errors.Add("Error. Purchase order detail is not found");
            }
            return prd;
        }
        
        public PurchaseReceivalDetail VUniquePOD(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IItemService _is)
        {
            IList<PurchaseReceivalDetail> details = _prds.GetObjectsByPurchaseReceivalId(prd.PurchaseReceivalId);
            foreach (var detail in details)
            {
                if (detail.PurchaseOrderDetailId == prd.PurchaseOrderDetailId && detail.Id != prd.Id)
                {
                     prd.Errors.Add("Error. Purchase order detail has more than one purchase receival detail in this purchase receival");
                }
            }
            return prd;
        }

        public PurchaseReceivalDetail VIsConfirmed(PurchaseReceivalDetail prd)
        {
            if (prd.IsConfirmed)
            {
                prd.Errors.Add("Error. Purchase receival detail is already confirmed");
            }
            return prd;
        }

        public PurchaseReceivalDetail VHasItemQuantity(PurchaseReceivalDetail prd, IItemService _is)
        {
            Item item = _is.GetObjectById(prd.ItemId);
            if (item.PendingReceival < 0)
            {
                prd.Errors.Add("Error. Item " + item.Name + " has pending receival less than zero");
            }
            if (item.Ready < prd.Quantity)
            {
                prd.Errors.Add("Error. Item " + item.Name + " has ready stock less than the quantity of the purchase receival detail");
            }
            return prd;
        }

        public PurchaseReceivalDetail VCreateObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IPurchaseReceivalService _prs,
                                                    IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is, IContactService _cs)
        {
            VHasPurchaseReceival(prd, _prs);
            VHasItem(prd, _is);
            if (!isValid(prd)) return prd;
            VCustomer(prd, _prs, _pos, _pods, _cs);
            if (!isValid(prd)) return prd;
            VQuantityCreate(prd, _pods);
            if (!isValid(prd)) return prd;
            VUniquePOD(prd, _prds, _is);
            return prd;
        }

        public PurchaseReceivalDetail VUpdateObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IPurchaseReceivalService _prs,
                                                    IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is, IContactService _cs)
        {
            VHasPurchaseReceival(prd, _prs);
            VHasItem(prd, _is);
            if (!isValid(prd)) return prd;
            VCustomer(prd, _prs, _pos, _pods, _cs);
            if (!isValid(prd)) return prd;
            VQuantityUpdate(prd, _pods);
            if (!isValid(prd)) return prd;
            VUniquePOD(prd, _prds, _is);
            if (!isValid(prd)) return prd;
            VIsConfirmed(prd);
            return prd;
        }

        public PurchaseReceivalDetail VDeleteObject(PurchaseReceivalDetail prd)
        {
            VIsConfirmed(prd);
            return prd;
        }

        public PurchaseReceivalDetail VConfirmObject(PurchaseReceivalDetail prd)
        {
            VIsConfirmed(prd);
            return prd;
        }

        public PurchaseReceivalDetail VUnconfirmObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IItemService _is)
        {
            VHasItemQuantity(prd, _is);
            return prd;
        }

        public bool ValidCreateObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IPurchaseReceivalService _prs,
                                                    IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is, IContactService _cs)
        {
            VCreateObject(prd, _prds, _prs, _pods, _pos, _is, _cs);
            return isValid(prd);
        }

        public bool ValidUpdateObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IPurchaseReceivalService _prs,
                                                    IPurchaseOrderDetailService _pods, IPurchaseOrderService _pos, IItemService _is, IContactService _cs)
        {
            VUpdateObject(prd, _prds, _prs, _pods, _pos, _is, _cs);
            return isValid(prd);
        }

        public bool ValidDeleteObject(PurchaseReceivalDetail prd)
        {
            VDeleteObject(prd);
            return isValid(prd);
        }

        public bool ValidConfirmObject(PurchaseReceivalDetail prd)
        {
            VConfirmObject(prd);
            return isValid(prd);
        }

        public bool ValidUnconfirmObject(PurchaseReceivalDetail prd, IPurchaseReceivalDetailService _prds, IItemService _is)
        {
            VUnconfirmObject(prd, _prds, _is);
            return isValid(prd);
        }

        public bool isValid(PurchaseReceivalDetail prd)
        {
            bool isValid = !prd.Errors.Any();
            return isValid;
        }

        public string PrintError(PurchaseReceivalDetail prd)
        {
            string erroroutput = prd.Errors.ElementAt(0);
            foreach (var item in prd.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}