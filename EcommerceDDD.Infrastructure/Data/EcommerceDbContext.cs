using Microsoft.EntityFrameworkCore;
using EcommerceDDD.Domain.Entities;
using EcommerceDDD.Domain.ValueObjects;

namespace EcommerceDDD.Infrastructure.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }

        public DbSet<IndividualPerson> IndividualPersons { get; set; }
        public DbSet<CorporatePerson> CorporatePersons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para herança de Person (TPH - Table Per Hierarchy)
            modelBuilder.Entity<Person>()
                .ToTable("Users")
                .HasDiscriminator<string>("PersonType")
                .HasValue<IndividualPerson>("Individual")
                .HasValue<CorporatePerson>("Corporate");

            // Configuração para IndividualPerson
            modelBuilder.Entity<IndividualPerson>(entity =>
            {
                entity.Property(e => e.CPF)
                    .HasConversion(
                        cpf => cpf.Value,
                        value => new CPF(value))
                    .HasColumnName("CPF")
                    .HasMaxLength(11);
            });

            // Configuração para CorporatePerson
            modelBuilder.Entity<CorporatePerson>(entity =>
            {
                entity.Property(e => e.CNPJ)
                    .HasConversion(
                        cnpj => cnpj.Value,
                        value => new CNPJ(value))
                    .HasColumnName("CNPJ")
                    .HasMaxLength(14);
            });

            // Configuração para Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Stock).IsRequired();
            });

            // Configuração para Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                
                // Relacionamento com Person
                entity.HasOne(e => e.Person)
                    .WithMany()
                    .HasForeignKey(e => e.PersonId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração para OrderProduct
            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("OrderProducts");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");

                // Relacionamento com Order
                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderProducts)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relacionamento com Product
                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurações gerais
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
} 