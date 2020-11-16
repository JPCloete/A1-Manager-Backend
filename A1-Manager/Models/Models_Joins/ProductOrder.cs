using A1_Manager.Models_Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Joins
{
    public class ProductOrder
    {
        public virtual int BranchProductId { get; set; }

        public virtual BranchProduct BranchProduct { get; set; }

        public virtual int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
