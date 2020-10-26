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
        public virtual int Id { get; set; }

        public virtual int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }

        public virtual float Amount { get; set; }
    }
}
