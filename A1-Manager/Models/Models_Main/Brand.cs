using A1_Manager.Main_Interfaces;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Main
{
    public class Brand : IBrand
    {
        public Brand()
        {
            Branches = new HashSet<Branch>();
            Products = new HashSet<Product>();
            BrandSales = new HashSet<BrandSale>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoURL { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public int DateAddedId { get; set; }

        [ForeignKey("DateAddedId")]
        public Date DateAdded { get; set; }

        public virtual ICollection<Branch>? Branches { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

        public virtual ICollection<BrandSale>? BrandSales { get; set; }

        public virtual ICollection<Role>? Roles { get; set; }
    }
}
