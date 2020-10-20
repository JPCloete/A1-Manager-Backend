using A1_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.JoinModels
{
    public class BranchSupplier
    {
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
