using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class Payable
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string PayableSource { get; set; }
        public int PayableSourceId { get; set; }
        public string Code { get; set; }

        public decimal Amount { get; set; }
        public decimal RemainingAmount { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVouchers { get; set; }

        public Dictionary<String, String> Errors { get; set; }
    }
}
