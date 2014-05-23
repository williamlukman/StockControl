using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IContactValidator
    {
        Contact VName(Contact c);
        Contact VAddress(Contact c);
        Contact VHasPurchaseOrder(Contact c, IPurchaseOrderService _pos);
        Contact VHasPurchaseReceival(Contact c, IPurchaseReceivalService _prs);
        Contact VHasSalesOrder(Contact c, ISalesOrderService _sos);
        Contact VHasDeliveryOrder(Contact c, IDeliveryOrderService _dos);
        Contact VCreateObject(Contact c);
        Contact VUpdateObject(Contact c);
        Contact VDeleteObject(Contact c, IPurchaseOrderService _pos, IPurchaseReceivalService _prs,
                                    ISalesOrderService _sos, IDeliveryOrderService _dos);
        bool ValidCreateObject(Contact c);
        bool ValidUpdateObject(Contact c);
        bool ValidDeleteObject(Contact c, IPurchaseOrderService _pos, IPurchaseReceivalService _prs,
                                    ISalesOrderService _sos, IDeliveryOrderService _dos);
        bool isValid(Contact c);
        string PrintError(Contact c);
    }
}
