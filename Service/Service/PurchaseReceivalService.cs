using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
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
        private IPurchaseReceivalValidator _validator;

        public PurchaseReceivalService(IPurchaseReceivalRepository _purchaseReceivalRepository, IPurchaseReceivalValidator _purchaseReceivalValidator)
        {
            _p = _purchaseReceivalRepository;
            _validator = _purchaseReceivalValidator;
        }

        public IPurchaseReceivalValidator GetValidator()
        {
            return _validator;
        }

        public IList<PurchaseReceival> GetAll()
        {
            return _p.GetAll();
        }

        public PurchaseReceival GetObjectById(int Id)
        {
            return _p.GetObjectById(Id);
        }

        public IList<PurchaseReceival> GetObjectsByContactId(int contactId)
        {
            return _p.GetObjectsByContactId(contactId);
        }
        
        public PurchaseReceival CreateObject(PurchaseReceival purchaseReceival, IContactService _contactService)
        {
            return (_validator.ValidCreateObject(purchaseReceival, _contactService) ? _p.CreateObject(purchaseReceival) : purchaseReceival);
        }

        public PurchaseReceival CreateObject(int contactId, DateTime receivalDate, IContactService _contactService)
        {
            PurchaseReceival pr = new PurchaseReceival
            {
                CustomerId = contactId,
                ReceivalDate = receivalDate
            };
            return this.CreateObject(pr, _contactService);
        }

        public PurchaseReceival UpdateObject(PurchaseReceival purchaseReceival, IContactService _contactService)
        {
            return (_validator.ValidUpdateObject(purchaseReceival, _contactService) ? _p.UpdateObject(purchaseReceival) : purchaseReceival);
        }

        public PurchaseReceival SoftDeleteObject(PurchaseReceival purchaseReceival, IPurchaseReceivalDetailService _purchaseReceivalDetailService)
        {
            return (_validator.ValidDeleteObject(purchaseReceival, _purchaseReceivalDetailService) ? _p.SoftDeleteObject(purchaseReceival) : purchaseReceival);
        }

        public bool DeleteObject(int Id)
        {
            return _p.DeleteObject(Id);
        }

        public PurchaseReceival ConfirmObject(PurchaseReceival purchaseReceival, IPurchaseReceivalDetailService _prds,
                                                IPurchaseOrderDetailService _pods, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidConfirmObject(purchaseReceival, _prds))
            {
                _p.ConfirmObject(purchaseReceival);
                IList<PurchaseReceivalDetail> details = _prds.GetObjectsByPurchaseReceivalId(purchaseReceival.Id);
                foreach (var detail in details)
                {
                    _prds.ConfirmObject(detail, _stockMutationService, _itemService);
                    PurchaseOrderDetail pod = _pods.GetObjectById(detail.PurchaseOrderDetailId);
                    _pods.FulfilObject(pod);
                }
            }
            return purchaseReceival;
        }

        public PurchaseReceival UnconfirmObject(PurchaseReceival purchaseReceival, IPurchaseReceivalDetailService _prds,
                                                IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidUnconfirmObject(purchaseReceival, _prds, _itemService))
            {
                _p.UnconfirmObject(purchaseReceival);
                IList<PurchaseReceivalDetail> details = _prds.GetObjectsByPurchaseReceivalId(purchaseReceival.Id);
                foreach (var detail in details)
                {
                    _prds.UnconfirmObject(detail, _stockMutationService, _itemService);
                }
            }
            return purchaseReceival;
        }
    }
}