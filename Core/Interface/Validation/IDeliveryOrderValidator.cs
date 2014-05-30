using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IDeliveryOrderValidator
    {
        DeliveryOrder VCustomer(DeliveryOrder d, IContactService _cs);
        DeliveryOrder VDeliveryDate(DeliveryOrder d);
        DeliveryOrder VIsConfirmed(DeliveryOrder d);
        DeliveryOrder VHasDeliveryOrderDetails(DeliveryOrder d, IDeliveryOrderDetailService _dods);
        DeliveryOrder VCreateObject(DeliveryOrder d, IContactService _cs);
        DeliveryOrder VUpdateObject(DeliveryOrder d, IContactService _cs);
        DeliveryOrder VDeleteObject(DeliveryOrder d, IDeliveryOrderDetailService _dods);
        DeliveryOrder VConfirmObject(DeliveryOrder d, IDeliveryOrderDetailService _dods, IItemService _is);
        DeliveryOrder VUnconfirmObject(DeliveryOrder d, IDeliveryOrderDetailService _dods, IItemService _is);
        bool ValidCreateObject(DeliveryOrder d, IContactService _cs);
        bool ValidUpdateObject(DeliveryOrder d, IContactService _cs);
        bool ValidDeleteObject(DeliveryOrder d, IDeliveryOrderDetailService _dods);
        bool ValidConfirmObject(DeliveryOrder d, IDeliveryOrderDetailService _dods, IItemService _is);
        bool ValidUnconfirmObject(DeliveryOrder d, IDeliveryOrderDetailService _dods, IItemService _is);
        bool isValid(DeliveryOrder d);
        string PrintError(DeliveryOrder d);
    }
}
