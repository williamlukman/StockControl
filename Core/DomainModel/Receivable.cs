﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class Receivable
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string ReceivableSource { get; set; }
        public int ReceivableSourceId { get; set; }
        public string Code { get; set; }

        public decimal Amount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal PendingClearanceAmount { get; set; }
        public bool IsCompleted { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ICollection<ReceiptVoucherDetail> ReceiptVoucherDetails { get; set; }

        public Dictionary<String, String> Errors { get; set; }
    }
}
