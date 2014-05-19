using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public bool IsConfirmed { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public HashSet<string> Errors { get; set; }

    }
}