using A1_Manager.Main_Interfaces;
using A1_Manager.Models_Joins;
using A1_Manager.Models_Main;
using A1_Manager.Models_Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace A1_Manager.Models_Main
{
    public class Branch : IBranch
    {
        public Branch()
        {
            Suppliers = new HashSet<BranchSupplier>();
            Products = new HashSet<BranchProduct>();
            Sales = new HashSet<BranchSale>();
            Employees = new HashSet<Employee>();
            Orders = new HashSet<Order>();
        }

        public virtual int Id { get; set; }

        public virtual int NameId { get; set; }

        [ForeignKey("NameId")]
        public virtual Identity Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Telephone { get; set; }

        public virtual int DateAddedId { get; set; }

        [ForeignKey("DateAddedId")]
        public virtual Date DateAdded { get; set; }

        public virtual int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual string Address { get; set; }

        public virtual int PreferredCurrencyId { get; set; }

        [ForeignKey("PreferredCurrencyId")]
        public virtual Currency PreferredCurrency { get; set; }

        public virtual int OccupancyCostId { get; set; }

        [ForeignKey("OccupancyCostId")]
        public virtual Money OccupancyCost { get; set; }

        public virtual int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<BranchSupplier>? Suppliers { get; set; }

        public virtual ICollection<BranchProduct>? Products { get; set; }

        public virtual ICollection<BranchSale>? Sales { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
