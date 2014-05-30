using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IDeliveryOrderDetailValidator
    {
        DeliveryOrderDetail VHasDeliveryOrder(DeliveryOrderDetail dod, IDeliveryOrderService _dos);
        DeliveryOrderDetail VHasItem(DeliveryOrderDetail dod, IItemService _is);
        DeliveryOrderDetail VCustomer(DeliveryOrderDetail dod, IDeliveryOrderService _dos, ISalesOrderService _sos, ISalesOrderDetailService _sods, IContactService _cs);
        DeliveryOrderDetail VQuantityCreate(DeliveryOrderDetail dod, ISalesOrderDetailService _sods);
        DeliveryOrderDetail VQuantityUpdate(DeliveryOrderDetail dod, ISalesOrderDetailService _sods);
        DeliveryOrderDetail VQuantityUnconfirm(DeliveryOrderDetail dod, IItemService _is);
        DeliveryOrderDetail VHasSalesOrderDetail(DeliveryOrderDetail dod, ISalesOrderDetailService _sods);
        DeliveryOrderDetail VHasItemQuantity(DeliveryOrderDetail dod, IItemService _is);
        DeliveryOrderDetail VUniqueSOD(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IItemService _is);
        DeliveryOrderDetail VIsConfirmed(DeliveryOrderDetail dod);
        DeliveryOrderDetail VCreateObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IDeliveryOrderService _dos, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is, IContactService _cs);
        DeliveryOrderDetail VUpdateObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IDeliveryOrderService _dos, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is, IContactService _cs);
        DeliveryOrderDetail VDeleteObject(DeliveryOrderDetail dod);
        DeliveryOrderDetail VConfirmObject(DeliveryOrderDetail dod, IItemService _is);
        DeliveryOrderDetail VUnconfirmObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IItemService _is);
        bool ValidCreateObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IDeliveryOrderService _dos, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is, IContactService _cs);
        bool ValidUpdateObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IDeliveryOrderService _dos, ISalesOrderDetailService _sods, ISalesOrderService _sos, IItemService _is, IContactService _cs);
        bool ValidDeleteObject(DeliveryOrderDetail dod);
        bool ValidConfirmObject(DeliveryOrderDetail dod, IItemService _is);
        bool ValidUnconfirmObject(DeliveryOrderDetail dod, IDeliveryOrderDetailService _dods, IItemService _is);
        bool isValid(DeliveryOrderDetail dod);
        string PrintError(DeliveryOrderDetail dod);
    }
}
