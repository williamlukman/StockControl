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
    public class StockMutationRepository : EfRepository<StockMutation>, IStockMutationRepository
    {
        private StockControlEntities stocks;
        public StockMutationRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<StockMutation> GetAll()
        {
            List<StockMutation> sms = (from s in stocks.StockMutations
                                       where !s.IsDeleted
                                       select s).ToList();
            return sms;
        }

        IList<StockMutation> GetObjectsByItemId(int itemId)
        {
            List<StockMutation> sms = (from s in stocks.StockMutations
                                       where !s.IsDeleted && s.ItemId == itemId
                                       select s).ToList();
            return sms;
        }

        StockMutation GetObjectById(int Id)
        {
            StockMutation sm = (from s in stocks.StockMutations
                                where !s.IsDeleted && s.Id == Id
                                select s).FirstOrDefault();
            return sm;
        }

        StockMutation CreateObject(StockMutation stockMutation)
        {
            StockMutation sm = new StockMutation();
            sm.ItemId = stockMutation.ItemId;
            sm.ItemCase = stockMutation.ItemCase;
            sm.Status = stockMutation.Status;
            sm.SourceDocumentType = stockMutation.SourceDocumentType;
            sm.SourceDocumentDetailType = stockMutation.SourceDocumentDetailType;
            sm.SourceDocumentId = stockMutation.SourceDocumentId;
            sm.SourceDocumentDetailId = stockMutation.SourceDocumentDetailId;
            sm.Quantity = stockMutation.Quantity;
            sm.IsDeleted = false;
            sm.CreatedAt = DateTime.Now;
            return Create(sm);
        }

        StockMutation UpdateObject(StockMutation stockMutation)
        {
            StockMutation sm = new StockMutation();
            sm.ItemId = stockMutation.ItemId;
            sm.ItemCase = stockMutation.ItemCase;
            sm.Status = stockMutation.Status;
            sm.SourceDocumentType = stockMutation.SourceDocumentType;
            sm.SourceDocumentDetailType = stockMutation.SourceDocumentDetailType;
            sm.SourceDocumentId = stockMutation.SourceDocumentId;
            sm.SourceDocumentDetailId = stockMutation.SourceDocumentDetailId;
            sm.Quantity = stockMutation.Quantity;
            sm.ModifiedAt = DateTime.Now;
            Update(sm);
            return sm;
        }

        StockMutation SoftDeleteObject(StockMutation stockMutation)
        {
            stockMutation.IsDeleted = true;
            stockMutation.DeletedAt = DateTime.Now;
            Update(stockMutation);
            return stockMutation;
        }

        bool DeleteObject(int Id)
        {
            StockMutation sm = Find(x => x.Id == Id);
            return (Delete(sm) == 1) ? true : false;
        }

    }
}