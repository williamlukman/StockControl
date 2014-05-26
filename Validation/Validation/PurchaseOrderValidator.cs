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
    public class PurchaseOrderValidator : IPurchaseOrderValidator
    {
        public PurchaseOrder VCustomer(PurchaseOrder po, IContactService _cs)
        {
            Contact c = _cs.GetObjectById(po.CustomerId);
            if (c == null)
            {
                po.Errors.Add("Error. Customer does not exist");
            }
            return po;
        }

        public PurchaseOrder VPurchaseDate(PurchaseOrder po)
        {
            /* purchaseDate is never null
            if (po.PurchaseDate == null)
            {
                po.Errors.Add("Error. Purchase Date does not exist");
            }
            */
            return po;
        }

        public PurchaseOrder VIsConfirmed(PurchaseOrder po)
        {
            if (po.IsConfirmed)
            {
                po.Errors.Add("Error. Purchase Order is confirmed already");
            }
            return po;
        }

        public PurchaseOrder VHasPurchaseOrderDetails(PurchaseOrder po, IPurchaseOrderDetailService _pods)
        {
            IList<PurchaseOrderDetail> details = _pods.GetObjectsByPurchaseOrderId(po.Id);
            if (!details.Any())
            {
                po.Errors.Add("Error. Purchase Order does not have purchase order details");
            }
            return po;
        }

        public PurchaseOrder VHasItemPendingReceival(PurchaseOrder po, IPurchaseOrderDetailService _pods, IItemService _is)
        {
            IList<PurchaseOrderDetail> details = _pods.GetObjectsByPurchaseOrderId(po.Id);
            foreach (var detail in details)
            {
                Item item = _is.GetObjectById(detail.ItemId);
                if (item.PendingReceival < 0)
                {
                    po.Errors.Add ("Error. Item " + item.Name + " has pending receival less than 0");
                }
            }
            return po;
        }

        public PurchaseOrder VCreateObject(PurchaseOrder po, IContactService _cs)
        {
            VCustomer(po, _cs);
            VPurchaseDate(po);
            return po;
        }

        public PurchaseOrder VUpdateObject(PurchaseOrder po, IContactService _cs)
        {
            VCustomer(po, _cs);
            VPurchaseDate(po);
            VIsConfirmed(po);
            return po;
        }

        public PurchaseOrder VDeleteObject(PurchaseOrder po, IPurchaseOrderDetailService _pods)
        {
            VIsConfirmed(po);
            if (isValid(po))
            {
                IList<PurchaseOrderDetail> details = _pods.GetObjectsByPurchaseOrderId(po.Id);
                IPurchaseOrderDetailValidator detailvalidator = new PurchaseOrderDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VDeleteObject(detail);
                    po.Errors.UnionWith(detail.Errors);
                }
            }

            return po;
        }

        public PurchaseOrder VConfirmObject(PurchaseOrder po, IPurchaseOrderDetailService _pods)
        {
            VIsConfirmed(po);
            VHasPurchaseOrderDetails(po, _pods);
            if (isValid(po))
            {
                IList<PurchaseOrderDetail> details = _pods.GetObjectsByPurchaseOrderId(po.Id);
                IPurchaseOrderDetailValidator detailvalidator = new PurchaseOrderDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VConfirmObject(detail);
                    po.Errors.UnionWith(detail.Errors);
                }
            }
            return po;
        }

        public PurchaseOrder VUnconfirmObject(PurchaseOrder po, IPurchaseOrderDetailService _pods, IPurchaseReceivalDetailService _prds, IItemService _is)
        {
            VHasItemPendingReceival(po, _pods, _is);
            if (isValid(po))
            {
                IList<PurchaseOrderDetail> details = _pods.GetObjectsByPurchaseOrderId(po.Id);
                IPurchaseOrderDetailValidator detailvalidator = new PurchaseOrderDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VUnconfirmObject(detail, _pods, _prds, _is);
                    po.Errors.UnionWith(detail.Errors);
                }
            }

            return po;
        }

        public bool ValidCreateObject(PurchaseOrder po, IContactService _cs)
        {
            VCreateObject(po, _cs);
            return isValid(po);
        }

        public bool ValidUpdateObject(PurchaseOrder po, IContactService _cs)
        {
            VUpdateObject(po, _cs);
            return isValid(po);
        }

        public bool ValidDeleteObject(PurchaseOrder po, IPurchaseOrderDetailService _pods)
        {
            VDeleteObject(po, _pods);
            return isValid(po);
        }

        public bool ValidConfirmObject(PurchaseOrder po, IPurchaseOrderDetailService _pods)
        {
            VConfirmObject(po, _pods);
            return isValid(po);
        }

        public bool ValidUnconfirmObject(PurchaseOrder po, IPurchaseOrderDetailService _pods, IPurchaseReceivalDetailService _prds, IItemService _is)
        {
            VUnconfirmObject(po, _pods, _prds, _is);
            return isValid(po);
        }

        public bool isValid(PurchaseOrder po)
        {
            bool isValid = !po.Errors.Any();
            return isValid;
        }

        public string PrintError(PurchaseOrder po)
        {
            string erroroutput = "";
            foreach (var item in po.Errors)
            {
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}