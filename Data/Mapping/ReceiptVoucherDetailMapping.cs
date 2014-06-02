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
            HasRequired(rvd => rvd.ReceiptVoucher)
                .WithMany(rv => rv.ReceiptVoucherDetails)
                .HasForeignKey(rvd => rvd.ReceiptVoucherId);
            Ignore(i => i.Errors);
        }
    }
}
