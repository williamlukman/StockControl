﻿using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class StockAdjustmentMapping : EntityTypeConfiguration<StockAdjustment>
    {
        public StockAdjustmentMapping()
        {
            HasKey(sa => sa.Id);
            HasOptional(sa => sa.StockAdjustmentDetails);
            Ignore(sa => sa.Errors);
        }
    }
}
