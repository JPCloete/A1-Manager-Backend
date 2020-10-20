using A1_Manager.Interfaces;
using A1_Manager.JoinModels;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models
{
    public class ProductSale : ISale
    {

        public int Id { get; set; }

        public int BranchProductId { get; set; }

        public BranchProduct BranchProduct {get; set;}

        public int DateId { get; set; }

        [ForeignKey("DateId")]
        public Date Date { get; set; }

        public int? ExpensesId { get; set; }

        [ForeignKey("ExpensesId")]
        public virtual Money? Expenses { get; set; }

        public int? ProfitId { get; set; }

        [ForeignKey("ProfitId")]
        public virtual Money? Profit { get; set; }

        public int? RevenueId { get; set; }

        [ForeignKey("RevenueId")]
        public virtual Money? Revenue { get; set; }
    }
}
