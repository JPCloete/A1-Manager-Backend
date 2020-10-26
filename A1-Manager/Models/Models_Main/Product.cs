using A1_Manager.Main_Interfaces;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Main
{
    public class Product : IProduct
    {
        public Product()
        {
            Branches = new HashSet<BranchProduct>();
        }

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string ImageURL { get; set; }

        public virtual string BarCode { get; set; }

        public virtual string BarCodeImageURL { get; set; }

        public virtual int DateAddedId { get; set; }

        [ForeignKey("DateAddedId")]
        public virtual Date DateAdded { get; set; }

        public virtual int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<BranchProduct>? Branches { get; set; }

    }
}
