using A1_Manager.Main_Interfaces;
using System;
using System.Collections.Generic;
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

        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public string BarCode { get; set; }

        public string BarCodeImageURL { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public virtual ICollection<BranchProduct>? Branches { get; set; }

    }
}
