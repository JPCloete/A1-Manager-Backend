using A1_Manager.Main_Interfaces;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Main
{
    public class BrandSale : ISale
    {

        public virtual int Id { get; set; }

        public virtual int DateId { get; set; }

        [ForeignKey("DateId")]
        public virtual Date Date { get; set; }

        public virtual int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual int? ExpensesId { get; set; }

        [ForeignKey("ExpensesId")]
        public virtual Money? Expenses { get; set; }

        public virtual int? ProfitId { get; set; }

        [ForeignKey("ProfitId")]
        public virtual Money? Profit { get; set; }

        public virtual int? RevenueId { get; set; }

        [ForeignKey("RevenueId")]
        public virtual Money? Revenue { get; set; }

        public virtual int TaxId { get; set; }

        [ForeignKey("TaxId")]
        public virtual Money? Tax { get; set; }
    }
}
