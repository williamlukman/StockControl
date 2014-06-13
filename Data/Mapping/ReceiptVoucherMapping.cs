using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ReceiptVoucherMapping : EntityTypeConfiguration<ReceiptVoucher>
    {

        public ReceiptVoucherMapping()
        {
            HasKey(rv => rv.Id);
            HasRequired(rv => rv.Contact)
                .WithMany(c => c.ReceiptVouchers)
                .WillCascadeOnDelete(false);
            HasOptional(pr => pr.ReceiptVoucherDetails);
            Ignore(i => i.Errors);
        }
    }
}
