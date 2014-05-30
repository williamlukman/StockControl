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
    public class DeliveryOrderDetailValidator : IDeliveryOrderDetailValidator
    {
        public DeliveryOrderDetail VHasDeliveryOrder(DeliveryOrderDetail dod, IDeliveryOrderService _prs)
        {
            DeliveryOrder pr = _prs.GetObjectById(dod.DeliveryOrderId);
            if (pr == null)
            {
                dod.Errors.Add("Error. Delivery Order does not exist");
            }
            return dod;
        }

        public DeliveryOrderDetail VHasItem(DeliveryOrderDetail dod, IItemService _is)
        {
            Item item = _is.GetObjectById(dod.ItemId);
            if (item == null)
            {
                dod.Errors.Add("Error. Item does not exist");
            }
            return dod;
        }

        public DeliveryOrderDetail VCustomer(DeliveryOrderDetail dod, IDeliveryOrderService _prs, ISalesOrderService _sos, ISalesOrderDetailService _sods, IContactService _cs)
        {
            DeliveryOrder pr = _prs.GetObjectById(dod.DeliveryOrderId);
            SalesOrderDetail sod = _sods.GetObjectById(dod.SalesOrderDetailId);
            if (sod == null)
            {
                dod.Errors.Add("Error. Could not find associated sales order detail");
                return dod;
            }
            SalesOrder so = _sos.GetObjectById(sod.SalesOrderId);
            if (so.CustomerId != pr.CustomerId)
            {
                dod.Errors.Add("Error. Contact does not match of delivery order and sales order");
            }
            return dod;
        }

        public DeliveryOrderDetail VQuantityCreate(DeliveryOrderDetail dod, ISalesOrderDetailService _sods)
        {
            SalesOrderDetail sod = _sods.GetObjectById(dod.SalesOrderDetailId);
            if (dod.Quantity <= 0)
            {
                dod.Errors.Add("Error. Quantity must be greater than zero");
            }
            if (dod.Quantity > sod.Quantity)
            {
                dod.Errors.Add("Error. Quantity must be less than sales order's quantity of " + sod.Quantity);
            }
            return dod;
        }

        public DeliveryOrderDetail VQuantityUpdate(DeliveryOrderDetail dod, ISalesOrderDetailService _sods)
        {
            SalesOrderDetail sod = _sods.GetObjectById(dod.SalesOrderDetailId);
            if (dod.Quantity <= 0)
            {
                dod.Errors.Add("Error. Quantity must be greater or equal to zero");
            }
            if (dod.Quantity > sod.Quantity)
            {
                dod.Errors.Add("Error. Quantity must be less than sales order's quantity of " + sod.Quantity);
            }
            return dod;
        }

        public DeliveryOrderDetail VQuantityUnconfirm(DeliveryOrderDetail dod, IItemService _is)
        {
            Item item = _is.GetObjectById(dod.ItemId);
            if (item.PendingDelivery < 0)
            {
                dod.Errors.Add("Error. Item " + item.Name + " has pending delivery less than zero");
            }
            if (item.Ready < 0)
            {
                dod.Errors.Add("Error. Item " + item.Name + " has ready stock less than zero");
            }
            return dod;
        }

        public DeliveryOrderDetail VHasSalesOrderDetail(DeliveryOrderDetail dod, ISalesOrderDetailService _sods)
        {
            SalesOrderDetail sod = _sods.GetObjectById(dod.SalesOrderDetailId);
            if (sod == null)
            {
                dod.Errors.Add("Error. Sales order detail is not found");
            }
            return dod;
        }

        public DeliveryOrderDetail VHasItemQuantity(DeliveryOrderDetail dod, IItemService _is)
        {
            Item item = _is.GetObjectById(dod.ItemId);
            if (item.Ready - dod.Quantity < 0)
            {
                dod.Errors.Add("The quantity of item ready is less than the confirmed delivery order detail");
            }
            return dod;
        }

        public DeliveryOrderDetail VUniqueSOD(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IItemService _is)
        {
            IList<DeliveryOrderDetail> details = _dods.GetObjectsByDeliveryOrderId(dod.DeliveryOrderId);
            foreach (var detail in details)
            {
                if (detail.SalesOrderDetailId == dod.SalesOrderDetailId && detail.Id != dod.Id)
                {
                    dod.Errors.Add("Error. Sales order detail has more than one delivery order detail in this delivery order");
                    return dod;
                }
            }
            return dod;
        }

        public DeliveryOrderDetail VIsConfirmed(DeliveryOrderDetail dod)
        {
            if (dod.IsConfirmed)
            {
                dod.Errors.Add("Error. Delivery order detail is already confirmed");
            }
            return dod;
        }

        public DeliveryOrderDetail VCreateObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IDeliveryOrderService _prs,
                                                    ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is, IContactService _cs)
        {
            VHasDeliveryOrder(dod, _prs);
            VHasItem(dod, _is);
            if (!isValid(dod)) { return dod; }
            VCustomer(dod, _prs, _sos, _sods, _cs);
            if (!isValid(dod)) { return dod; }
            VQuantityCreate(dod, _sods);
            if (!isValid(dod)) { return dod; }
            VUniqueSOD(dod, _dods, _is);
            return dod;
        }

        public DeliveryOrderDetail VUpdateObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IDeliveryOrderService _prs,
                                                    ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is, IContactService _cs)
        {
            VHasDeliveryOrder(dod, _prs);
            VHasItem(dod, _is);
            if (!isValid(dod)) { return dod; }
            VCustomer(dod, _prs, _sos, _sods, _cs);
            if (!isValid(dod)) { return dod; }
            VQuantityUpdate(dod, _sods);
            if (!isValid(dod)) { return dod; }
            VUniqueSOD(dod, _dods, _is);
            if (!isValid(dod)) { return dod; }
            VIsConfirmed(dod);
            return dod;
        }

        public DeliveryOrderDetail VDeleteObject(DeliveryOrderDetail dod)
        {
            VIsConfirmed(dod);
            return dod;
        }

        public DeliveryOrderDetail VConfirmObject(DeliveryOrderDetail dod, IItemService _is)
        {
            VIsConfirmed(dod);
            VHasItemQuantity(dod, _is);
            return dod;
        }

        public DeliveryOrderDetail VUnconfirmObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IItemService _is)
        {
            VQuantityUnconfirm(dod, _is);
            return dod;
        }

        public bool ValidCreateObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IDeliveryOrderService _prs,
                                                    ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is, IContactService _cs)
        {
            VCreateObject(dod, _dods, _prs, _sods, _sos, _is, _cs);
            return isValid(dod);
        }

        public bool ValidUpdateObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IDeliveryOrderService _prs,
                                                    ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is, IContactService _cs)
        {
            VUpdateObject(dod, _dods, _prs, _sods, _sos, _is, _cs);
            return isValid(dod);
        }

        public bool ValidDeleteObject(DeliveryOrderDetail dod)
        {
            VDeleteObject(dod);
            return isValid(dod);
        }

        public bool ValidConfirmObject(DeliveryOrderDetail dod, IItemService _is)
        {
            VConfirmObject(dod, _is);
            return isValid(dod);
        }

        public bool ValidUnconfirmObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IItemService _is)
        {
            VUnconfirmObject(dod, _dods, _is);
            return isValid(dod);
        }

        public bool isValid(DeliveryOrderDetail dod)
        {
            bool isValid = !dod.Errors.Any();
            return isValid;
        }

        public string PrintError(DeliveryOrderDetail dod)
        {
            string erroroutput = dod.Errors.ElementAt(0);
            foreach (var item in dod.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}