using A1_Manager.Support_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Support
{
    public class Money : IMoney
    {
        public int Id { get; set; }

        public int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public float Amount { get; set; }
    }
}
