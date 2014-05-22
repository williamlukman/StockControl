using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PurchaseReceivalService : IPurchaseReceivalService
    {
        private IPurchaseReceivalRepository _p;
        public PurchaseReceivalService(IPurchaseReceivalRepository _purchaseReceivalRepository)
        {
            _p = _purchaseReceivalRepository;
        }

        public IList<PurchaseReceival> GetAll()
        {
            return _p.GetAll();
        }

        public PurchaseReceival GetObjectById(int Id)
        {
            return _p.GetObjectById(Id);
        }

        public PurchaseReceival CreateObject(PurchaseReceival purchaseReceival)
        {
            return _p.CreateObject(purchaseReceival);
        }

        public PurchaseReceival CreateObject(int contactId, DateTime receivalDate)
        {
            PurchaseReceival pr = new PurchaseReceival
            {
                CustomerId = contactId,
                ReceivalDate = receivalDate
            };
            return _p.CreateObject(pr);
        }

        public PurchaseReceival UpdateObject(PurchaseReceival purchaseReceival)
        {
            return _p.UpdateObject(purchaseReceival);
        }

        public PurchaseReceival SoftDeleteObject(PurchaseReceival purchaseReceival)
        {
            return _p.SoftDeleteObject(purchaseReceival);
        }

        public bool DeleteObject(int Id)
        {
            return _p.DeleteObject(Id);
        }

        public PurchaseReceival ConfirmObject(PurchaseReceival purchaseReceival)
        {
            return _p.ConfirmObject(purchaseReceival);
        }

        public PurchaseReceival UnconfirmObject(PurchaseReceival purchaseReceival)
        {
            return _p.UnconfirmObject(purchaseReceival);
        }
    }
}