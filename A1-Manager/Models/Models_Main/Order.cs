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
    public class Order : IOrder
    {
        public Order()
        {
            OrderStatus = 0;
            Products = new HashSet<ProductOrder>();
        }

        public virtual int Id { get; set; }

        public virtual int OrderStatus { get; set; }

        public virtual int SupplierId { get; set; }
   
        public virtual Supplier Supplier { get; set; }

        public virtual int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual int OrderedDateId { get; set; }

        [ForeignKey("OrderedDateId")]
        public virtual Date OrderedDate { get; set; }

        public virtual int DeliveryDateId { get; set; }

        [ForeignKey("DeliveryDateId")]
        public virtual Date DeliveryDate { get; set; }

        public virtual int AmountId { get; set; }

        [ForeignKey("AmountId")]
        public virtual Amount Amount { get; set; }

        public virtual int CostId { get; set; }

        [ForeignKey("CostId")]
        public virtual Money Cost { get; set; }

        public virtual ICollection<ProductOrder> Products { get; set; }
    }
}
