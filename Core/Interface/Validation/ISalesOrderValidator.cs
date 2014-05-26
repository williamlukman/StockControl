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
        SalesOrder VCustomer(SalesOrder so, IContactService _cs);
        SalesOrder VSalesDate(SalesOrder so);
        SalesOrder VIsConfirmed(SalesOrder so);
        SalesOrder VHasSalesOrderDetails(SalesOrder so, ISalesOrderDetailService _sods);
        SalesOrder VCreateObject(SalesOrder so, IContactService _cs);
        SalesOrder VUpdateObject(SalesOrder so, IContactService _cs);
        SalesOrder VDeleteObject(SalesOrder so, ISalesOrderDetailService _sods);
        SalesOrder VConfirmObject(SalesOrder so, ISalesOrderDetailService _sods);
        SalesOrder VUnconfirmObject(SalesOrder so, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is);
        bool ValidCreateObject(SalesOrder so, IContactService _cs);
        bool ValidUpdateObject(SalesOrder so, IContactService _cs);
        bool ValidDeleteObject(SalesOrder so, ISalesOrderDetailService _sods);
        bool ValidConfirmObject(SalesOrder so, ISalesOrderDetailService _sods);
        bool ValidUnconfirmObject(SalesOrder so, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is);
        bool isValid(SalesOrder so);
        string PrintError(SalesOrder so);
    }
}
