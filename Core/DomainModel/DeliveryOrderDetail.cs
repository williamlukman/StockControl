using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class DeliveryOrderDetail
    {
        public int Id { get; set; }
        public int DeliveryOrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public bool IsConfirmed { get; set; }
        public Nullable<DateTime> ConfirmedAt { get; set; }
        public bool IsFulfilled { get; set; }

        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        public virtual Item Item { get; set; }
        public virtual DeliveryOrder DeliveryOrder { get; set; }
        public virtual SalesOrderDetail SalesOrderDetail { get; set; }
        public HashSet<string> Errors { get; set; }
    }
}