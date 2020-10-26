using A1_Manager.Models_Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Joins
{
    public class BranchSupplier
    {
        public virtual int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
