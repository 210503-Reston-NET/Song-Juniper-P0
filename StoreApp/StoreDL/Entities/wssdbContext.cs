using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StoreDL.Entities
{
    public partial class wssdbContext : DbContext
    {
        public wssdbContext()
        {
        }

        public wssdbContext(DbContextOptions<wssdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<StoreFront> StoreFronts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cName");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__produ__45BE5BA9");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__store__44CA3770");
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineItems__order__4F47C5E3");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineItems__produ__503BEA1C");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasColumnName("dateCreated");

                entity.Property(e => e.Placed).HasColumnName("placed");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__customer__498EEC8D");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__storeId__4A8310C6");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.PDesc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pDesc");

                entity.Property(e => e.PName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pName");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<StoreFront>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sAddress");

                entity.Property(e => e.SName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
