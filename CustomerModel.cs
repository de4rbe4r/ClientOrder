using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Test_ClientOrder
{
    public partial class CustomerModel : DbContext
    {
        public CustomerModel()
            : base("name=CustomerModel")
        {
        }

        public virtual DbSet<CustomerDB> CustomerDBs { get; set; }
        public virtual DbSet<OrderDB> OrderDBs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDB>()
                .HasMany(e => e.OrderDBs)
                .WithOptional(e => e.CustomerDB)
                .HasForeignKey(e => e.CustomerId);

            modelBuilder.Entity<OrderDB>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
