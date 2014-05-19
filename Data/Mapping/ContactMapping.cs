using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ContactMapping : EntityTypeConfiguration<Contact>
    {
        public ContactMapping()
        {
            HasKey(x => x.Id);
            HasMany(x => x.PurchaseOrders)
                .WithRequired(y => y.Customer)
                .Map(x => x.MapKey("Id"));
            Ignore(x => x.Errors);
        }
    }
}
