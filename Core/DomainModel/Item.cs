using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        //public virtual ICollection<StockMutation> StockMutations { get; set; }
        //public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public HashSet<string> Errors { get; set; }
    }
}