using A1_Manager.Interfaces;
using A1_Manager.JoinModels;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models
{
    public class Supplier : ISupplier
    {
        public Supplier()
        {
            Products = new HashSet<BranchProduct>();
            Orders = new HashSet<Order>();
            Branches = new HashSet<BranchSupplier>();
        }

        public int Id { get; set; }

        public int IdentityId { get; set; }

        [ForeignKey("IdentityId")]
        public Identity Identity { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public string Address { get; set; }

        public virtual ICollection<BranchProduct>? Products { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

        public virtual ICollection<BranchSupplier> Branches { get; set; }
    }
}
