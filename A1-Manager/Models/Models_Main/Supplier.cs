using A1_Manager.Main_Interfaces;
using A1_Manager.Models_Joins;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Main
{
    public class Supplier : ISupplier
    {
        public Supplier()
        {
            Products = new HashSet<BranchProduct>();
            Orders = new HashSet<Order>();
            Branches = new HashSet<BranchSupplier>();
        }

        public virtual int Id { get; set; }

        public virtual int IdentityId { get; set; }

        [ForeignKey("IdentityId")]
        public virtual Identity Identity { get; set; }

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

        public virtual ICollection<BranchProduct>? Products { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

        public virtual ICollection<BranchSupplier> Branches { get; set; }
    }
}
