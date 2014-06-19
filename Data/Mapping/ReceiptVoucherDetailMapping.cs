using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ReceiptVoucherDetailMapping : EntityTypeConfiguration<ReceiptVoucherDetail>
    {

        public ReceiptVoucherDetailMapping()
        {
            HasKey(rvd => rvd.Id);
            HasRequired(rvd => rvd.Contact)
                .WithMany(c => c.ReceiptVoucherDetails)
                .WillCascadeOnDelete(false);
            HasRequired(rvd => rvd.ReceiptVoucher)
                .WithMany(pv => pv.ReceiptVoucherDetails)
                .HasForeignKey(rvd => rvd.ReceiptVoucherId);
            HasRequired(rvd => rvd.Receivable)
                .WithMany(p => p.ReceiptVoucherDetails)
                .HasForeignKey(rvd => rvd.ReceivableId);
            Ignore(i => i.Errors);
        }
    }
}
