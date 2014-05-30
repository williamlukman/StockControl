using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface ISalesOrderDetailValidator
    {
        SalesOrderDetail VHasSalesOrder(SalesOrderDetail sod, ISalesOrderService _pos);
        SalesOrderDetail VHasItem(SalesOrderDetail sod, IItemService _is);
        SalesOrderDetail VQuantity(SalesOrderDetail sod);
        SalesOrderDetail VPrice(SalesOrderDetail sod);
        SalesOrderDetail VUniqueSOD(SalesOrderDetail sod, ISalesOrderDetailService _sods, IItemService _is);
        SalesOrderDetail VIsConfirmed(SalesOrderDetail sod);
        SalesOrderDetail VHasItemPendingDelivery(SalesOrderDetail sod, IItemService _is);
        SalesOrderDetail VConfirmedDeliveryOrder(SalesOrderDetail sod, IDeliveryOrderDetailService _dods);
        SalesOrderDetail VCreateObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is);
        SalesOrderDetail VUpdateObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is);
        SalesOrderDetail VDeleteObject(SalesOrderDetail sod);
        SalesOrderDetail VConfirmObject(SalesOrderDetail sod);
        SalesOrderDetail VUnconfirmObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is);
        bool ValidCreateObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is);
        bool ValidUpdateObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is);
        bool ValidDeleteObject(SalesOrderDetail sod);
        bool ValidConfirmObject(SalesOrderDetail sod);
        bool ValidUnconfirmObject(SalesOrderDetail sod, ISalesOrderDetailService _sods, IDeliveryOrderDetailService _dods, IItemService _is);
        bool isValid(SalesOrderDetail sod);
        string PrintError(SalesOrderDetail sod);
    }
}
