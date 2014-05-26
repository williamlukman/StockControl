using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseOrderService
    {
        IPurchaseOrderValidator GetValidator();
        IList<PurchaseOrder> GetAll();
        PurchaseOrder GetObjectById(int Id);
        IList<PurchaseOrder> GetObjectsByContactId(int contactId);
        PurchaseOrder CreateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder CreateObject(int contactId, DateTime purchaseDate);
        PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder);
        bool DeleteObject(int Id);
        PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderDetailService _pods,
                                    IStockMutationService _stockMutationService, IItemService _itemService);
        PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderDetailService _pods,
                                    IPurchaseReceivalDetailService _purchaseReceivalDetailService, IStockMutationService _stockMutationService, IItemService _itemService);
    }
}