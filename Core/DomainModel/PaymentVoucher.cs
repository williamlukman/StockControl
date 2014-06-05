﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class PaymentVoucher
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int PayableId { get; set; }
        public string Code { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PendingClearanceAmount { get; set; }
        public bool IsConfirmed { get; set; }
        public Nullable<DateTime> ConfirmedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime PaymentDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public virtual Payable Payable { get; set; }
        public virtual ICollection<PaymentVoucherDetail> PaymentVoucherDetails { get; set; }

        public Dictionary<String, String> Errors { get; set; }
    }
}