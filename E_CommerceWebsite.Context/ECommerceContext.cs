using E_CommerceWebsite.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceWebsite.Context
{
    public class ECommerceContext : IdentityDbContext
    {
        // depency injection below allows me to :configure and pass the DbContextOptions to the base class constructor
        //This enables you to configure the database connection and other options in the (Program.cs file)
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {

        }


        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Orders> Orders{ get; set; }

        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdersProducts>()
                .HasOne(or => or.Orders).WithMany(orpr => orpr.OredersProducts).HasForeignKey(orpr => orpr.OrdersID);
            modelBuilder.Entity<OrdersProducts>()
                .HasOne(pr => pr.Products).WithMany(pror => pror.OredersProducts).HasForeignKey(pror => pror.ProductID);
            modelBuilder.Entity<OrdersProducts>()
                .HasKey(po => new { po.ProductID, po.OrdersID});

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });
        }
    }
}