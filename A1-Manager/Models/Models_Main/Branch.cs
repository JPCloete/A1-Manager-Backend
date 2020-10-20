using A1_Manager.Interfaces;
using A1_Manager.JoinModels;
using A1_Manager.Models_Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace A1_Manager.Models
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

        public int Id { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public string Address { get; set; }

        public int OccupancyCostId { get; set; }

        [ForeignKey("OccupancyCostId")]
        public Money OccupancyCost { get; set; } 

        public virtual ICollection<BranchSupplier>? Suppliers { get; set; }

        public virtual ICollection<BranchProduct>? Products { get; set; }

        public virtual ICollection<BranchSale>? Sales { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
