using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Collections.Generic;
using Core.DomainModel;

namespace Data.Context
{
    public class StockControlEntities : DbContext
    {
        public StockControlEntities()
            : base ("name=StockControlEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Item> Items{get; set;}
        public DbSet<PurchaseOrder> PurchaseOrders{get; set;}
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails{get; set;}
        public DbSet<StockMutation> StockMutations{get; set;}
    }
}