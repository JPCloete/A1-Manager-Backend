using A1_Manager.Main_Interfaces;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Main
{
    public class Contract : IContract
    {
        public int Id { get; set; }

        public string ContractPdfURL { get; set; }

        public int SignedDateId { get; set; }

        [ForeignKey("SignedDateId")]
        public Date SignedDate { get; set; }

        public int ExpirationDateId { get; set; }

        [ForeignKey("ExpirationDateId")]
        public Date ExpirationDate { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
