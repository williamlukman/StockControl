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
    public class SalesOrderValidator : ISalesOrderValidator
    {
        public SalesOrder VCustomer(SalesOrder so, IContactService _cs)
        {
            Contact c = _cs.GetObjectById(so.CustomerId);
            if (c == null)
            {
                so.Errors.Add("Error. Customer does not exist");
            }
            return so;
        }

        public SalesOrder VSalesDate(SalesOrder so)
        {
            /* salesDate is never null
            if (so.SalesDate == null)
            {
                so.Errors.Add("Error. Sales Date does not exist");
            }
            */
            return so;
        }

        public SalesOrder VIsConfirmed(SalesOrder so)
        {
            if (so.IsConfirmed)
            {
                so.Errors.Add("Error. Sales Order is confirmed already");
            }
            return so;
        }

        public SalesOrder VHasSalesOrderDetails(SalesOrder so, ISalesOrderDetailService _sods)
        {
            IList<SalesOrderDetail> details = _sods.GetObjectsBySalesOrderId(so.Id);
            if (!details.Any())
            {
                so.Errors.Add("Error. Sales Order does not have sales order details");
            }
            return so;
        }

        public SalesOrder VCreateObject(SalesOrder so, IContactService _cs)
        {
            VCustomer(so, _cs);
            VSalesDate(so);
            return so;
        }

        public SalesOrder VUpdateObject(SalesOrder so, IContactService _cs)
        {
            VCustomer(so, _cs);
            VSalesDate(so);
            VIsConfirmed(so);
            return so;
        }

        public SalesOrder VDeleteObject(SalesOrder so, ISalesOrderDetailService _sods)
        {
            VConfirmObject(so, _sods);
            return so;
        }

        public SalesOrder VConfirmObject(SalesOrder so, ISalesOrderDetailService _sods)
        {
            VIsConfirmed(so);
            VHasSalesOrderDetails(so, _sods);
            if (isValid(so))
            {
                IList<SalesOrderDetail> details = _sods.GetObjectsBySalesOrderId(so.Id);
                ISalesOrderDetailValidator detailvalidator = new SalesOrderDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VConfirmObject(detail);
                    so.Errors.UnionWith(detail.Errors);
                }
            }
            return so;
        }

        public SalesOrder VUnconfirmObject(SalesOrder so, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is)
        {
            if (isValid(so))
            {
                IList<SalesOrderDetail> details = _sods.GetObjectsBySalesOrderId(so.Id);
                ISalesOrderDetailValidator detailvalidator = new SalesOrderDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VUnconfirmObject(detail, _sods, _dods, _is);
                    so.Errors.UnionWith(detail.Errors);
                }
            }
            return so;
        }

        public bool ValidCreateObject(SalesOrder so, IContactService _cs)
        {
            VCreateObject(so, _cs);
            return isValid(so);
        }

        public bool ValidUpdateObject(SalesOrder so, IContactService _cs)
        {
            VUpdateObject(so, _cs);
            return isValid(so);
        }

        public bool ValidDeleteObject(SalesOrder so, ISalesOrderDetailService _sods)
        {
            VDeleteObject(so, _sods);
            return isValid(so);
        }

        public bool ValidConfirmObject(SalesOrder so, ISalesOrderDetailService _sods)
        {
            VConfirmObject(so, _sods);
            return isValid(so);
        }

        public bool ValidUnconfirmObject(SalesOrder so, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is)
        {
            VUnconfirmObject(so, _sods, _dods, _is);
            return isValid(so);
        }

        public bool isValid(SalesOrder so)
        {
            bool isValid = !so.Errors.Any();
            return isValid;
        }

        public string PrintError(SalesOrder so)
        {
            string erroroutput = "";
            foreach (var item in so.Errors)
            {
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}