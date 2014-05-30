using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class StockAdjustmentDetailMapping : EntityTypeConfiguration<StockAdjustmentDetail>
    {
        public StockAdjustmentDetailMapping()
        {
            HasKey(sad => sad.Id);
            HasRequired(sad => sad.Item)
                .WithMany(i => i.StockAdjustmentDetails)
                .HasForeignKey(sad => sad.ItemId);
            HasRequired(sad => sad.StockAdjustment)
                .WithMany(sa => sa.StockAdjustmentDetails)
                .HasForeignKey(sad => sad.StockAdjustmentId);
            Ignore(sad => sad.Errors);
        }
    }
}
