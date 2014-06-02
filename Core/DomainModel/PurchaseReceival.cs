using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class PurchaseReceival
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime ReceivalDate { get; set; }

        public bool IsConfirmed { get; set; }
        public Nullable<DateTime> ConfirmedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ICollection<PurchaseReceivalDetail> PurchaseReceivalDetails { get; set; }
        public Dictionary<String, String> Errors { get; set; }

    }
}