using A1_Manager.Models.Models_Main;
using A1_Manager.Models_Joins;
using A1_Manager.Models_Main;
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
           
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BranchSupplier>(bp =>
            {
                bp.HasKey(x => new { x.BranchId, x.SupplierId });

                bp.HasOne(x => x.Branch)
                    .WithMany(y => y.Suppliers)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Cascade);

                bp.HasOne(x => x.Supplier)
                    .WithMany(y => y.Branches)
                    .HasForeignKey(x => x.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EmployeeRole>(bs =>
            {
                bs.HasKey(x => new { x.EmployeeId, x.RoleId });

                bs.HasOne(x => x.Employee)
                    .WithMany(y => y.Roles)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Role)
                    .WithMany(y => y.Employees)
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductOrder>(bp =>
            {
                bp.HasKey(x => new { x.BranchProductId, x.OrderId });

                bp.HasOne(x => x.BranchProduct)
                    .WithMany(y => y.Orders)
                    .HasForeignKey(x => x.BranchProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                bp.HasOne(x => x.Order)
                    .WithMany(y => y.Products)
                    .HasForeignKey(x => x.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
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
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(x => x.Sales)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasMany(x => x.Employees)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(x => x.Orders)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(x => x.Suppliers)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(x => x.Products)
                    .WithOne(y => y.Branch)
                    .HasForeignKey(y => y.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.City)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Country)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.PreferredCurrency)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.DateAdded)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Name)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.OccupancyCost)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BranchProduct>(bp =>
            {
                bp.HasKey(x => x.Id);

                bp.HasOne(x => x.Branch)
                    .WithMany(y => y.Products)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);

                bp.HasOne(x => x.Product)
                    .WithMany(y => y.Branches)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                bp.HasMany(x => x.Sales)
                    .WithOne(y => y.BranchProduct)
                    .HasForeignKey(x => x.BranchProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                bp.HasOne(x => x.Stock)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bp.HasOne(x => x.DateAdded)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bp.HasOne(x => x.Cost)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bp.HasOne(x => x.RetailPrice)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BranchSale>(bs =>
            {
                bs.HasKey(x => x.Id);

                bs.HasOne(x => x.Branch)
                    .WithMany(y => y.Sales)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Date)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Expenses)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Profit)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Revenue)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Tax)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Brand>(b =>
            {
                b.HasKey(x => x.Id);

                b.HasMany(x => x.Branches)
                    .WithOne(y => y.Brand)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(x => x.Products)
                    .WithOne(y => y.Brand)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(x => x.BrandSales)
                    .WithOne(y => y.Brand)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(x => x.Roles)
                    .WithOne(y => y.Brand)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.PreferredCurrency)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.DateAdded)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BrandSale>(bs =>
            {
                bs.HasKey(x => x.Id);

                bs.HasOne(x => x.Brand)
                    .WithMany(y => y.BrandSales)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Date)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Expenses)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Profit)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Revenue)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                bs.HasOne(x => x.Tax)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<City>(c =>
            {
                c.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Contract>(c =>
            {
                c.HasKey(x => x.Id);

                c.HasOne(x => x.SignedDate)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                c.HasOne(x => x.ExpirationDate)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Country>(c =>
            {
                c.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Currency>(c =>
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

                e.HasOne(x => x.Branch)
                    .WithMany(y => y.Employees)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Roles)
                    .WithOne(y => y.Employee)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(x => x.Presence)
                    .WithOne(y => y.Employee)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Contract)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.FirstName)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.LastName)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Salary)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmployeePresence>(ep =>
            {
                ep.HasKey(x => x.Id);

                ep.HasOne(x => x.Employee)
                    .WithMany(y => y.Presence)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);

                ep.HasOne(x => x.ClockInTime)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                ep.HasOne(x => x.ClockOutTime)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Identity>(i =>
            {
                i.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Money>(m =>
            {
                m.HasKey(x => x.Id);

                m.HasOne(x => x.Currency)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MoneyPerAmount>(mpa =>
            {
                mpa.HasKey(x => x.Id);

                mpa.HasOne(x => x.Amount)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                mpa.HasOne(x => x.Money)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Order>(o =>
            {
                o.HasKey(x => x.Id);

                o.HasOne(x => x.Supplier)
                    .WithMany(y => y.Orders)
                    .HasForeignKey(x => x.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                o.HasOne(x => x.Branch)
                    .WithMany(y => y.Orders)
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Cascade);

                o.HasMany(x => x.Products)
                    .WithOne(y => y.Order)
                    .HasForeignKey(x => x.BranchProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                o.HasOne(x => x.MoneyPerAmount)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                o.HasOne(x => x.OrderedDate)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                o.HasOne(x => x.DeliveryDate)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.HasKey(x => x.Id);

                p.HasMany(x => x.Branches)
                    .WithOne(y => y.Product)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                p.HasOne(x => x.Brand)
                    .WithMany(y => y.Products)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);

                p.HasOne(x => x.DateAdded)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<ProductSale>(ps =>
            {
                ps.HasKey(x => x.Id);

                ps.HasOne(x => x.Date)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                ps.HasOne(x => x.Expenses)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                ps.HasOne(x => x.Profit)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                ps.HasOne(x => x.Revenue)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                ps.HasOne(x => x.Tax)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Role>(r =>
            {
                r.HasKey(x => x.Id);

                r.HasMany(x => x.Employees)
                    .WithOne(y => y.Role)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                r.HasOne(x => x.Brand)
                    .WithMany(y => y.Roles)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);

                r.HasOne(x => x.Name)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Supplier>(s =>
            {
                s.HasKey(x => x.Id);

                s.HasMany(x => x.Orders)
                    .WithOne(y => y.Supplier)
                    .HasForeignKey(x => x.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);

                s.HasMany(x => x.Branches)
                    .WithOne(y => y.Supplier)
                    .HasForeignKey(x => x.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(x => x.Products)
                    .WithOne(y => y.Supplier)
                    .HasForeignKey(x => x.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(x => x.City)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(x => x.Country)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(x => x.DateAdded)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(x => x.Name)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        //JoinModels
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

        public DbSet<EmployeePresence> EmployeePresence { get; set; }

        public DbSet<Identity> Identities { get; set; }

        public DbSet<Money> Money { get; set; }

        public DbSet<MoneyPerAmount> MoneyPerAmount { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSale> ProductSales { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
    }
}
