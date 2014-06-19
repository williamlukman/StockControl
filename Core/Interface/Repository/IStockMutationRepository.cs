using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IStockMutationRepository : IRepository<StockMutation>
    {
        IList<StockMutation> GetAll();
        IList<StockMutation> GetObjectsByItemId(int itemId);
        StockMutation GetObjectById(int Id);
        IList<StockMutation> GetObjectsBySourceDocumentDetail(int itemId, string SourceDocumentDetailType, int SourceDocumentDetailId);
        StockMutation CreateObject(StockMutation stockMutation);
        StockMutation UpdateObject(StockMutation stockMutation);
        StockMutation SoftDeleteObject(StockMutation stockMutation);
        bool DeleteObject(int Id);
    }
}