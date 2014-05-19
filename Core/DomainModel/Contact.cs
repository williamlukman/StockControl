using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public HashSet<string> Errors { get; set; }
    }
}