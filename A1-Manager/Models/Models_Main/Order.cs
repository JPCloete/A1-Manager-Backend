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

        public int Id { get; set; }

        public int OrderStatus { get; set; }

        public int SupplierId { get; set; }
   
        public virtual Supplier Supplier { get; set; }

        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public int OrderedDateId { get; set; }

        [ForeignKey("OrderedDateId")]
        public Date OrderedDate { get; set; }

        public int DeliveryDateId { get; set; }

        [ForeignKey("DeliveryDateId")]
        public Date DeliveryDate { get; set; }

        public int AmountId { get; set; }

        [ForeignKey("AmountId")]
        public Amount Amount { get; set; }

        public int CostId { get; set; }

        [ForeignKey("CostId")]
        public virtual Money Cost { get; set; }

        public ICollection<ProductOrder> Products { get; set; }
    }
}
