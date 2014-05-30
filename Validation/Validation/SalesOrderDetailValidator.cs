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
    public class SalesOrderDetailValidator : ISalesOrderDetailValidator
    {
        public SalesOrderDetail VHasSalesOrder(SalesOrderDetail sod, ISalesOrderService _sos)
        {
            SalesOrder so = _sos.GetObjectById(sod.SalesOrderId);
            if (so == null)
            {
                sod.Errors.Add("Error. Sales Order does not exist");
            }
            return sod;
        }

        public SalesOrderDetail VHasItem(SalesOrderDetail sod, IItemService _is)
        {
            Item item = _is.GetObjectById(sod.ItemId);
            if (item == null)
            {
                sod.Errors.Add("Error. Item does not exist");
            }
            return sod;
        }

        public SalesOrderDetail VQuantity(SalesOrderDetail sod)
        {
            if (sod.Quantity < 0)
            {
                sod.Errors.Add("Error. Quantity must be greater than or equal to zero");
            }
            return sod;
        }

        public SalesOrderDetail VPrice(SalesOrderDetail sod)
        {
            if (sod.Price <= 0)
            {
                sod.Errors.Add("Error. Price must be greater than zero");
            }
            return sod;
        }

        public SalesOrderDetail VUniqueSOD(SalesOrderDetail sod, ISalesOrderDetailService _sods, IItemService _is)
        {
            IList<SalesOrderDetail> details = _sods.GetObjectsBySalesOrderId(sod.SalesOrderId);
            foreach (var detail in details)
            {
                if (detail.ItemId == sod.ItemId)
                {
                    sod.Errors.Add("Error. Sales order detail is not unique in this sales order");
                    return sod;
                }
            }
            return sod;
        }

        public SalesOrderDetail VIsConfirmed(SalesOrderDetail sod)
        {
            if (sod.IsConfirmed)
            {
                sod.Errors.Add("Error. Sales order detail is already confirmed");
            }
            return sod;
        }

        public SalesOrderDetail VHasItemPendingDelivery(SalesOrderDetail sod, IItemService _is)
        {
            Item item = _is.GetObjectById(sod.ItemId);
            if (item.PendingDelivery < sod.Quantity)
            {
                sod.Errors.Add("Error. Current item pending delivery is less than the quantity of sales order detail");
            }
            return sod;
        }

        public SalesOrderDetail VConfirmedDeliveryOrder(SalesOrderDetail sod, IDeliveryOrderDetailService _dods)
        {
            DeliveryOrderDetail dod = _dods.GetObjectBySalesOrderDetailId(sod.Id);
            if (dod == null)
            {
                return sod;
            }
            if (dod.IsConfirmed)
            {
                sod.Errors.Add("Error. Associated delivery order is confirmed already");
            }
            return sod;
        }

        public SalesOrderDetail VCreateObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is)
        {
            VHasSalesOrder(sod, _sos);
            VHasItem(sod, _is);
            VQuantity(sod);
            VPrice(sod);
            VUniqueSOD(sod, _sods, _is);
            return sod;
        }

        public SalesOrderDetail VUpdateObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is)
        {
            VHasSalesOrder(sod, _sos);
            VHasItem(sod, _is);
            VQuantity(sod);
            VPrice(sod);
            VUniqueSOD(sod, _sods, _is);
            VIsConfirmed(sod);
            return sod;
        }

        public SalesOrderDetail VDeleteObject(SalesOrderDetail sod)
        {
            VIsConfirmed(sod);
            return sod;
        }

        public SalesOrderDetail VConfirmObject(SalesOrderDetail sod)
        {
            VIsConfirmed(sod);
            return sod;
        }

        public SalesOrderDetail VUnconfirmObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is)
        {
            VHasItemPendingDelivery(sod, _is);
            VConfirmedDeliveryOrder(sod, _dods);
            return sod;
        }

        public bool ValidCreateObject(SalesOrderDetail sod,  ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is)
        {
            VCreateObject(sod, _sods, _sos, _is);
            return isValid(sod);
        }

        public bool ValidUpdateObject(SalesOrderDetail sod,  ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is)
        {
            VUpdateObject(sod, _sods, _sos, _is);
            return isValid(sod);
        }

        public bool ValidDeleteObject(SalesOrderDetail sod)
        {
            VDeleteObject(sod);
            return isValid(sod);
        }

        public bool ValidConfirmObject(SalesOrderDetail sod)
        {
            VConfirmObject(sod);
            return isValid(sod);
        }

        public bool ValidUnconfirmObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is)
        {
            VUnconfirmObject(sod, _sods, _dods, _is);
            return isValid(sod);
        }

        public bool isValid(SalesOrderDetail sod)
        {
            bool isValid = !sod.Errors.Any();
            return isValid;
        }

        public string PrintError(SalesOrderDetail sod)
        {
            string erroroutput = "";
            foreach (var item in sod.Errors)
            {
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}