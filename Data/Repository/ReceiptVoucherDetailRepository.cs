using Core.DomainModel;
using Core.Interface.Repository;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ReceiptVoucherDetailRepository : EfRepository<ReceiptVoucherDetail>, IReceiptVoucherDetailRepository
    {
        private StockControlEntities stocks;
        public ReceiptVoucherDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<ReceiptVoucherDetail> GetObjectsByReceiptVoucherId(int receiptVoucherId)
        {
            return FindAll(rvd => rvd.ReceiptVoucherId == receiptVoucherId && !rvd.IsDeleted).ToList();
        }

        public IList<ReceiptVoucherDetail> GetObjectsByReceivableId(int receivableId)
        {
            return FindAll(rvd => rvd.ReceivableId == receivableId && !rvd.IsDeleted).ToList();
        }

        public ReceiptVoucherDetail GetObjectById(int Id)
        {
            ReceiptVoucherDetail detail = Find(rvd => rvd.Id == Id);
            if (detail != null) { detail.Errors = new Dictionary<string, string>(); }
            return detail;
        }

        public ReceiptVoucherDetail CreateObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            receiptVoucherDetail.IsConfirmed = false;
            receiptVoucherDetail.IsDeleted = false;
            receiptVoucherDetail.CreatedAt = DateTime.Now;
            return Create(receiptVoucherDetail);
        }

        public ReceiptVoucherDetail UpdateObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            receiptVoucherDetail.ModifiedAt = DateTime.Now;
            Update(receiptVoucherDetail);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail SoftDeleteObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            receiptVoucherDetail.IsDeleted = true;
            receiptVoucherDetail.DeletedAt = DateTime.Now;
            Update(receiptVoucherDetail);
            return receiptVoucherDetail;
        }

        public bool DeleteObject(int Id)
        {
            ReceiptVoucherDetail rvd = Find(x => x.Id == Id);
            return (Delete(rvd) == 1) ? true : false;
        }

        public ReceiptVoucherDetail ConfirmObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            receiptVoucherDetail.IsConfirmed = true;
            Update(receiptVoucherDetail);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail UnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            receiptVoucherDetail.IsConfirmed = false;
            Update(receiptVoucherDetail);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail ClearObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            receiptVoucherDetail.IsCleared = true;
            Update(receiptVoucherDetail);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail UnclearObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            receiptVoucherDetail.IsCleared = false;
            Update(receiptVoucherDetail);
            return receiptVoucherDetail;
        }

    }
}