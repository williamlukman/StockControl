using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface ISalesOrderValidator
    {
        SalesOrder VCustomer(SalesOrder po, IContactService _cs);
        SalesOrder VSalesDate(SalesOrder po);
        SalesOrder VIsConfirmed(SalesOrder po);
        SalesOrder VHasSalesOrderDetails(SalesOrder po, ISalesOrderDetailService _sods);
        SalesOrder VHasItemPendingDelivery(SalesOrder po, ISalesOrderDetailService _sods, IItemService _is);
        SalesOrder VCreateObject(SalesOrder po, IContactService _cs);
        SalesOrder VUpdateObject(SalesOrder po, IContactService _cs);
        SalesOrder VDeleteObject(SalesOrder po, ISalesOrderDetailService _sods);
        SalesOrder VConfirmObject(SalesOrder po, ISalesOrderDetailService _sods);
        SalesOrder VUnconfirmObject(SalesOrder po, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is);
        bool ValidCreateObject(SalesOrder po, IContactService _cs);
        bool ValidUpdateObject(SalesOrder po, IContactService _cs);
        bool ValidDeleteObject(SalesOrder po, ISalesOrderDetailService _sods);
        bool ValidConfirmObject(SalesOrder po, ISalesOrderDetailService _sods);
        bool ValidUnconfirmObject(SalesOrder po, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is);
        bool isValid(SalesOrder po);
        string PrintError(SalesOrder po);
    }
}
