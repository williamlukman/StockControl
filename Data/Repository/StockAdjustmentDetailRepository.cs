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
    public class StockAdjustmentDetailRepository : EfRepository<StockAdjustmentDetail>, IStockAdjustmentDetailRepository
    {
        private StockControlEntities stocks;
        public StockAdjustmentDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<StockAdjustmentDetail> GetObjectsByStockAdjustmentId(int stockAdjustmentId)
        {
            return FindAll(sad => sad.StockAdjustmentId == stockAdjustmentId && !sad.IsDeleted).ToList();
        }

        public StockAdjustmentDetail GetObjectById(int Id)
        {
            StockAdjustmentDetail detail = Find(sad => sad.Id == Id && !sad.IsDeleted);
            detail.Errors = new Dictionary<string, string>();
            return detail;
        }

        public StockAdjustmentDetail CreateObject(StockAdjustmentDetail stockAdjustmentDetail)
        {
            stockAdjustmentDetail.IsConfirmed = false;
            stockAdjustmentDetail.IsDeleted = false;
            stockAdjustmentDetail.CreatedAt = DateTime.Now;
            return Create(stockAdjustmentDetail);
        }

        public StockAdjustmentDetail UpdateObject(StockAdjustmentDetail stockAdjustmentDetail)
        {
            stockAdjustmentDetail.ModifiedAt = DateTime.Now;
            Update(stockAdjustmentDetail);
            return stockAdjustmentDetail;
        }

        public StockAdjustmentDetail SoftDeleteObject(StockAdjustmentDetail stockAdjustmentDetail)
        {
            stockAdjustmentDetail.IsDeleted = true;
            stockAdjustmentDetail.DeletedAt = DateTime.Now;
            Update(stockAdjustmentDetail);
            return stockAdjustmentDetail;
        }

        public bool DeleteObject(int Id)
        {
            StockAdjustmentDetail sad = Find(x => x.Id == Id);
            return (Delete(sad) == 1) ? true : false;
        }

        public StockAdjustmentDetail ConfirmObject(StockAdjustmentDetail stockAdjustmentDetail)
        {
            stockAdjustmentDetail.IsConfirmed = true;
            Update(stockAdjustmentDetail);
            return stockAdjustmentDetail;
        }

        public StockAdjustmentDetail UnconfirmObject(StockAdjustmentDetail stockAdjustmentDetail)
        {
            stockAdjustmentDetail.IsConfirmed = false;
            Update(stockAdjustmentDetail);
            return stockAdjustmentDetail;
        }
    }
}