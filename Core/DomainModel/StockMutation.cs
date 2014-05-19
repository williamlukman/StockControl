using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public class StockMutation
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ItemCase { get; set; }
        public int Status { get; set; }

        public string SourceDocumentType { get; set; }
        public string SourceDocumentDetailType { get; set; }
        public int SourceDocumentId { get; set; }
        public int SourceDocumentDetailId { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public HashSet<string> Errors { get; set; }

    }
}