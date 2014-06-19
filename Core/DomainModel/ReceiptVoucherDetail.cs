using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class ReceiptVoucherDetail
    {
        public int Id { get; set; }
        public int ReceiptVoucherId { get; set; }
        public int ReceivableId { get; set; }
        public string Code { get; set; }
        // ContactId is a derivative from ReceiptVoucher
        public int ContactId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsInstantClearance { get; set; }

        public bool IsConfirmed { get; set; }
        public Nullable<DateTime> ConfirmedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCleared { get; set; }
        public Nullable<DateTime> ClearanceDate { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ReceiptVoucher ReceiptVoucher { get; set; }
        public virtual Receivable Receivable { get; set; }

        public Dictionary<String, String> Errors { get; set; }
    }
}