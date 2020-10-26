using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Support
{
    public class MoneyPerAmount
    {
        public virtual int Id { get; set; }

        public virtual int MoneyId { get; set; }

        [ForeignKey("MoneyId")]
        public virtual Money Money { get; set; }

        public virtual int AmountId { get; set; }

        public virtual Amount Amount { get; set; }
    }
}
