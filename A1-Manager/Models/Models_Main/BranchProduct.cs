using A1_Manager.Models_Joins;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Main
{
    public class BranchProduct
    {
        public int Id { get; set; }

        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public int CostId { get; set; }

        [ForeignKey("CostId")]
        public virtual Money Cost { get; set; }

        public int RetailPriceId { get; set; }

        [ForeignKey("RetailPriceId")]
        public virtual Money RetailPrice { get; set; }

        public int StockId { get; set; }

        [ForeignKey("StockId")]
        public virtual Amount? Stock { get; set; }

        public virtual ICollection<ProductSale>? Sales { get; set; }

        public virtual ICollection<ProductOrder>? Orders { get; set; }
    }
}
