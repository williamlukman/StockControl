using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class StockAdjustment
    {
        public int Id { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public bool IsConfirmed { get; set; }
        public Nullable<DateTime> ConfirmedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        public virtual ICollection<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public Dictionary<String, String> Errors { get; set; }
    }
}