using A1_Manager.Main_Interfaces;
using A1_Manager.Models.Models_Main;
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

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string LogoURL { get; set; }

        public virtual string Email { get; set; }

        public virtual string Telephone { get; set; }

        public virtual int DateAddedId { get; set; }

        [ForeignKey("DateAddedId")]
        public virtual Date DateAdded { get; set; }

        public virtual int PreferredCurrencyId { get; set; }

        [ForeignKey("PreferredCurrencyId")]
        public virtual Currency PreferredCurrency { get; set; }

        public virtual int SettingsId { get; set; }

        public virtual Settings Settings { get; set; }

        public virtual ICollection<Branch>? Branches { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

        public virtual ICollection<BrandSale>? BrandSales { get; set; }

        public virtual ICollection<Role>? Roles { get; set; }
    }
}
