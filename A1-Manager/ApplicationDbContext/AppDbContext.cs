using A1_Manager.JoinModels;
using A1_Manager.Models;
using A1_Manager.Models_Support;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //https://stackoverflow.com/questions/46526230/disable-cascade-delete-on-ef-core-2-globally
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BranchSupplier>(bp =>
            {
                bp.HasKey(x => new { x.BranchId, x.SupplierId });

                bp.HasOne(x => x.Branch)
                    .WithMany(y => y.Suppliers)
                    .HasForeignKey(x => x.BranchId);

                bp.HasOne(x => x.Supplier)
                    .WithMany(y => y.Branches)
                    .HasForeignKey(x => x.SupplierId);
            });

            modelBuilder.Entity<EmployeeRole>(bs =>
            {
                bs.HasKey(x => new { x.EmployeeId, x.RoleId });

                bs.HasOne(x => x.Employee)
                    .WithMany(y => y.Roles)
                    .HasForeignKey(x => x.EmployeeId);

                bs.HasOne(x => x.Role)
                    .WithMany(y => y.Employees)
                    .HasForeignKey(x => x.RoleId);
            });

            modelBuilder.Entity<ProductOrder>(bp =>
            {
                bp.HasKey(x => new { x.BranchProductId, x.OrderId });

                bp.HasOne(x => x.BranchProduct)
                    .WithMany(y => y.Orders)
                    .HasForeignKey(x => x.BranchProductId);

                bp.HasOne(x => x.Order)
                    .WithMany(y => y.Products)
                    .HasForeignKey(x => x.OrderId);
            });

            modelBuilder.Entity<Amount>(a =>
            {
                a.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Branch>(b =>
            {
                b.HasKey(x => x.Id);

                b.HasOne(x => x.Brand)
                    .WithMany(y => y.Branches)
                    .HasForeignKey(x => x.BrandId);

                b.HasOne(x => x.Country)
                    .WithOne()
                    .HasForeignKey<Branch>(x => x.CountryId);

                b.HasOne(x => x.City);

                b.HasMany(x => x.Sales)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(x => x.BranchId);

                b.HasMany(x => x.Employees)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(x => x.BranchId);

                b.HasMany(x => x.Orders)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(x => x.BranchId);

                b.HasMany(x => x.Suppliers)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(x => x.BranchId);

                b.HasMany(x => x.Products)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(y => y.BranchId);
            });

            modelBuilder.Entity<BranchProduct>(bp =>
            {
                bp.HasKey(x => x.Id);

                bp.HasOne(x => x.Branch)
                    .WithMany(y => y.Products)
                    .HasForeignKey(x => x.BranchId);

                bp.HasOne(x => x.Product)
                    .WithMany(y => y.Branches)
                    .HasForeignKey(x => x.ProductId);

                bp.HasMany(x => x.Sales)
                    .WithOne(y => y.BranchProduct)
                    .HasForeignKey(x => x.BranchProductId);
            });

            modelBuilder.Entity<BranchSale>(bs =>
            {
                bs.HasKey(x => x.Id);

                bs.HasOne(x => x.Date)
                    .WithOne()
                    .HasForeignKey<BranchSale>(x => x.DateId);

                bs.HasOne(x => x.Branch)
                    .WithMany(y => y.Sales)
                    .HasForeignKey(x => x.BranchId);

                bs.HasOne(x => x.Expenses)
                    .WithOne()
                    .HasForeignKey<BranchSale>(x => x.ExpensesId);

                bs.HasOne(x => x.Profit)
                    .WithOne()
                    .HasForeignKey<BranchSale>(x => x.ProfitId);

                bs.HasOne(x => x.Revenue)
                    .WithOne()
                    .HasForeignKey<BranchSale>(x => x.RevenueId);
            });

            modelBuilder.Entity<Brand>(b =>
            {
                b.HasKey(x => x.Id);

                b.HasOne(x => x.DateAdded)
                    .WithOne()
                    .HasForeignKey<Brand>(x => x.DateAddedId);

                b.HasMany(x => x.Branches)
                    .WithOne(y => y.Brand)
                    .HasForeignKey(x => x.BrandId);

                b.HasMany(x => x.Products)
                    .WithOne(y => y.Brand)
                    .HasForeignKey(x => x.BrandId);

                b.HasMany(x => x.BrandSales)
                    .WithOne(y => y.Brand)
                    .HasForeignKey(x => x.BrandId);

                b.HasMany(x => x.Roles)
                    .WithOne(y => y.Brand)
                    .HasForeignKey(x => x.BrandId);
            });

            modelBuilder.Entity<BrandSale>(bs =>
            {
                bs.HasKey(x => x.Id);

                bs.HasOne(x => x.Brand)
                    .WithMany(y => y.BrandSales)
                    .HasForeignKey(x => x.BrandId);
            });

            modelBuilder.Entity<City>(c =>
            {
                c.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Contract>(c =>
            {
                c.HasKey(x => x.Id);

                c.HasOne(x => x.Employee)
                    .WithOne(y => y.Contract)
                    .HasForeignKey<Contract>(x => x.EmployeeId);
            });

            modelBuilder.Entity<Country>(c =>
            {
                c.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Date>(d =>
            {
                d.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Employee>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.Contract)
                    .WithOne(y => y.Employee)
                    .HasForeignKey<Employee>(x => x.ContractId);

                e.HasOne(x => x.Branch)
                    .WithMany(y => y.Employees)
                    .HasForeignKey(x => x.BranchId);

                e.HasMany(x => x.Roles)
                    .WithOne(y => y.Employee)
                    .HasForeignKey(x => x.EmployeeId);
            });

            modelBuilder.Entity<Identity>(i =>
            {
                i.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Money>(m =>
            {
                m.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Order>(o =>
            {
                o.HasKey(x => x.Id);

                o.HasOne(x => x.Supplier)
                    .WithMany(y => y.Orders)
                    .HasForeignKey(x => x.SupplierId);

                o.HasOne(x => x.Branch)
                    .WithMany(y => y.Orders)
                    .HasForeignKey(x => x.BranchId);

                o.HasMany(x => x.Products)
                    .WithOne(y => y.Order)
                    .HasForeignKey(x => x.BranchProductId);
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.HasKey(x => x.Id);

                p.HasMany(x => x.Branches)
                    .WithOne(y => y.Product)
                    .HasForeignKey(x => x.ProductId);

                p.HasOne(x => x.Brand)
                    .WithMany(y => y.Products)
                    .HasForeignKey(x => x.BrandId);
            });

            modelBuilder.Entity<ProductSale>(ps =>
            {
                ps.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Role>(r =>
            {
                r.HasKey(x => x.Id);

                r.HasMany(x => x.Employees)
                    .WithOne(y => y.Role)
                    .HasForeignKey(x => x.EmployeeId);
            });

            modelBuilder.Entity<Supplier>(s =>
            {
                s.HasKey(x => x.Id);

                s.HasMany(x => x.Orders)
                    .WithOne(y => y.Supplier)
                    .HasForeignKey(x => x.SupplierId);

                s.HasMany(x => x.Branches)
                    .WithOne(y => y.Supplier)
                    .HasForeignKey(x => x.SupplierId);

                s.HasMany(x => x.Products)
                    .WithOne(y => y.Supplier)
                    .HasForeignKey(x => x.SupplierId);
            });
        }

        //JoinModels
        public DbSet<BranchProduct> BranchProducts { get; set; }

        public DbSet<BranchSupplier> BranchSuppliers { get; set; }

        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        //Models
        public DbSet<Amount> Amount { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<BranchSale> BranchSales { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<BrandSale> BrandSales { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Date> Dates { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Identity> Identities { get; set; }

        public DbSet<Money> Money { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSale> ProductSales { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
    }
}
