using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class PaymentVoucherDetail
    {
        public int Id { get; set; }
        public int PaymentVoucherId { get; set; }
        public int PayableId { get; set; }
        public string Code { get; set; }
        // ContactId is a derivative from PaymentVoucher
        public int ContactId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }

        public bool IsConfirmed { get; set; }
        public Nullable<DateTime> ConfirmedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCleared { get; set; }
        public Nullable<DateTime> ClearanceDate { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual PaymentVoucher PaymentVoucher { get; set; }
        public virtual Payable Payable { get; set; }

        public Dictionary<String, String> Errors { get; set; }
    }
}