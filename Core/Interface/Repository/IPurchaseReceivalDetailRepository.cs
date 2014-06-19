using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IPurchaseReceivalDetailRepository : IRepository<PurchaseReceivalDetail>
    {
        IList<PurchaseReceivalDetail> GetObjectsByPurchaseReceivalId(int purchaseReceivalId);
        PurchaseReceivalDetail GetObjectById(int Id);
        PurchaseReceivalDetail GetObjectByPurchaseOrderDetailId(int purchaseOrderDetailId);
        PurchaseReceivalDetail CreateObject(PurchaseReceivalDetail purchaseReceivalDetail);
        PurchaseReceivalDetail UpdateObject(PurchaseReceivalDetail purchaseReceivalDetail);
        PurchaseReceivalDetail SoftDeleteObject(PurchaseReceivalDetail purchaseReceivalDetail);
        bool DeleteObject(int Id);
        PurchaseReceivalDetail ConfirmObject(PurchaseReceivalDetail purchaseReceivalDetail);
        PurchaseReceivalDetail UnconfirmObject(PurchaseReceivalDetail purchaseReceivalDetail);
        string SetObjectCode(string ParentCode);
    }
}