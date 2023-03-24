using System.Data.Entity;
using WebBanLaptop.Migrations;
using WebBanLaptop.Models;

namespace WebBanLaptop
{
    public class LQShopDb : DbContext
    {
        public LQShopDb() : base("LQShopConnectionString")
        {
            
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<ReplyFeedback> ReplyFeedbacks { get; set; }
        public virtual DbSet<ProductImages> ProductImages { get; set; }
        public virtual DbSet<OrderAddress> OrderAddresses { get; set; }
        public virtual DbSet<AccountAddress> AccountAddresses { get; set; }
        public virtual DbSet<Wards> Wards { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Order_Detail> Order_Detail { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);


            modelBuilder.Entity<Account>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Feedbacks)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.Create_by)
                .IsUnicode(false);


            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.Create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Delivery)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Feedback>()
                .Property(e => e.Rate_star);


            modelBuilder.Entity<Feedback>()
                .Property(e => e.Create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.Create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_Detail>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Order_Detail>()
                .Property(e => e.Create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_Detail)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.Create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Payment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price);


            modelBuilder.Entity<Product>()
                .Property(e => e.Quantity)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Feedbacks)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => new { e.Product_id, e.Genre_id, e.Disscount_id })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_Detail)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => new { e.Product_id, e.Genre_id, e.Disscount_id })
                .WillCascadeOnDelete(false);

        }
    }
}