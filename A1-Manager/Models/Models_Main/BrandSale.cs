using A1_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models
{
    public class BrandSale : ISale
    {

        public int Id { get; set; }

        public int DateId { get; set; }

        [ForeignKey("DateId")]
        public Date Date { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public int? ExpensesId { get; set; }

        [ForeignKey("ExpensesId")]
        public virtual Money? Expenses { get; set; }

        public int? ProfitId { get; set; }

        [ForeignKey("ExpensesId")]
        public virtual Money? Profit { get; set; }

        public int? RevenueId { get; set; }

        [ForeignKey("ExpensesId")]
        public virtual Money? Revenue { get; set; }
    }
}
