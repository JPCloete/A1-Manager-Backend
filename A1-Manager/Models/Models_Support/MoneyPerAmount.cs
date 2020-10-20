using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Support
{
    public class MoneyPerAmount
    {
        public int Id { get; set; }

        public int MoneyId { get; set; }

        [ForeignKey("MoneyId")]
        public Money Money { get; set; }

        public int AmountId { get; set; }

        public Amount Amount { get; set; }
    }
}
