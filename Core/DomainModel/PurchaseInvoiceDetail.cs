using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class PurchaseInvoiceDetail
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int PurchaseReceivalDetailId { get; set; }
        public string Code { get; set; }
        public int ContactId { get; set; }

        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }

        public Dictionary<String, String> Errors { get; set; }
    }
}