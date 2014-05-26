using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;
using Core.Constant;

namespace Validation.Validation
{
    public class DeliveryOrderValidator : IDeliveryOrderValidator
    {

        public DeliveryOrder VCustomer(DeliveryOrder d, IContactService _cs)
        {
            Contact c = _cs.GetObjectById(d.CustomerId);
            if (c == null)
            {
                d.Errors.Add("Error. Customer does not exist");
            }
            return d;
        }

        public DeliveryOrder VDeliveryDate(DeliveryOrder d)
        {
            /* deliveryDate is never null
            if (d.DeliveryDate == null)
            {
                d.Errors.Add("Error. Delivery Date does not exist");
            }
            */
            return d;
        }

        public DeliveryOrder VIsConfirmed(DeliveryOrder d)
        {
            if (d.IsConfirmed)
            {
                d.Errors.Add("Error. Delivery Order is confirmed already");
            }
            return d;
        }

        public DeliveryOrder VHasDeliveryOrderDetails(DeliveryOrder d, IDeliveryOrderDetailService _dds)
        {
            IList<DeliveryOrderDetail> details = _dds.GetObjectsByDeliveryOrderId(d.Id);
            if (!details.Any())
            {
                d.Errors.Add("Error. Delivery Order does not have delivery order details");
            }
            return d;
        }

        public DeliveryOrder VHasItemQuantity(DeliveryOrder d, IDeliveryOrderDetailService _dds, IItemService _is)
        {
            IList<DeliveryOrderDetail> details = _dds.GetObjectsByDeliveryOrderId(d.Id);
            foreach (var detail in details)
            {
                Item item = _is.GetObjectById(detail.ItemId);
                if (item.Ready < 0)
                {
                    d.Errors.Add ("Error. Item " + item.Name + " has ready stock less than zero");
                }
            }
            return d;
        }

        public DeliveryOrder VCreateObject(DeliveryOrder d, IContactService _cs)
        {
            VCustomer(d, _cs);
            VDeliveryDate(d);
            return d;
        }

        public DeliveryOrder VUpdateObject(DeliveryOrder d, IContactService _cs)
        {
            VCustomer(d, _cs);
            VDeliveryDate(d);
            VIsConfirmed(d);
            return d;
        }

        public DeliveryOrder VDeleteObject(DeliveryOrder d, IDeliveryOrderDetailService _dds)
        {
            VConfirmObject(d, _dds);
            return d;
        }

        public DeliveryOrder VConfirmObject(DeliveryOrder d, IDeliveryOrderDetailService _dds)
        {
            VIsConfirmed(d);
            VHasDeliveryOrderDetails(d, _dds);
            if (isValid(d))
            {
                IList<DeliveryOrderDetail> details = _dds.GetObjectsByDeliveryOrderId(d.Id);
                IDeliveryOrderDetailValidator detailvalidator = new DeliveryOrderDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VConfirmObject(detail);
                    d.Errors.UnionWith(detail.Errors);
                }
            }
            return d;
        }

        public DeliveryOrder VUnconfirmObject(DeliveryOrder d, IDeliveryOrderDetailService _dds, IItemService _is)
        {
            
            VHasItemQuantity(d, _dds, _is);
            if (isValid(d))
            {
                IList<DeliveryOrderDetail> details = _dds.GetObjectsByDeliveryOrderId(d.Id);
                IDeliveryOrderDetailValidator detailvalidator = new DeliveryOrderDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VUnconfirmObject(detail, _dds, _is);
                    d.Errors.UnionWith(detail.Errors);
                }
            }

            return d;
        }

        public bool ValidCreateObject(DeliveryOrder d, IContactService _cs)
        {
            VCreateObject(d, _cs);
            return isValid(d);
        }

        public bool ValidUpdateObject(DeliveryOrder d, IContactService _cs)
        {
            VUpdateObject(d, _cs);
            return isValid(d);
        }

        public bool ValidDeleteObject(DeliveryOrder d, IDeliveryOrderDetailService _dds)
        {
            VDeleteObject(d, _dds);
            return isValid(d);
        }

        public bool ValidConfirmObject(DeliveryOrder d, IDeliveryOrderDetailService _dds)
        {
            VConfirmObject(d, _dds);
            return isValid(d);
        }

        public bool ValidUnconfirmObject(DeliveryOrder d, IDeliveryOrderDetailService _dds, IItemService _is)
        {
            VUnconfirmObject(d, _dds, _is);
            return isValid(d);
        }

        public bool isValid(DeliveryOrder d)
        {
            bool isValid = !d.Errors.Any();
            return isValid;
        }

        public string PrintError(DeliveryOrder d)
        {
            string erroroutput = "";
            foreach (var item in d.Errors)
            {
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}