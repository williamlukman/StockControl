﻿using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PayableMapping : EntityTypeConfiguration<Payable>
    {

        public PayableMapping()
        {
            HasKey(p => p.Id);
            HasRequired(p => p.Contact)
                .WithMany(c => c.Payables)
                .WillCascadeOnDelete(false);
            HasOptional(p => p.PaymentVoucherDetails);
            Ignore(p => p.Errors);
        }
    }
}
