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
        public virtual int Id { get; set; }

        public virtual int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual int DateAddedId { get; set; }

        [ForeignKey("DateAddedId")]
        public virtual Date DateAdded { get; set; }

        public virtual int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual int CostId { get; set; }

        [ForeignKey("CostId")]
        public virtual Money Cost { get; set; }

        public virtual int RetailPriceId { get; set; }

        [ForeignKey("RetailPriceId")]
        public virtual MoneyPerAmount RetailPrice { get; set; }

        public virtual int TaxPercentage { get; set; }

        public virtual int StockId { get; set; }

        [ForeignKey("StockId")]
        public virtual Amount? Stock { get; set; }

        public virtual int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ICollection<ProductSale>? Sales { get; set; }

        public virtual ICollection<ProductOrder>? Orders { get; set; }
    }
}
