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

        public DbSet<Contact> contacts { get; set; }
        public DbSet<Item> items{get; set;}
        public DbSet<PurchaseOrder> purchaseOrders{get; set;}
        public DbSet<PurchaseOrderDetail> purchaseOrderDetails{get; set;}
        public DbSet<StockMutation> stockMutations{get; set;}
    }
}