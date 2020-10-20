using A1_Manager.Models_Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Joins
{
    public class ProductOrder
    {
        public int BranchProductId { get; set; }

        public BranchProduct BranchProduct { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
