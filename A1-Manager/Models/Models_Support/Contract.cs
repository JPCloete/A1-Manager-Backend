using A1_Manager.Main_Interfaces;
using A1_Manager.Models_Support;
using A1_Manager.Support_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Support
{
    public class Contract : IContract
    {
        public virtual int Id { get; set; }

        public virtual string PdfURL { get; set; }

        public virtual int SignedDateId { get; set; }

        [ForeignKey("SignedDateId")]
        public virtual Date SignedDate { get; set; }

        public virtual int ExpirationDateId { get; set; }

        [ForeignKey("ExpirationDateId")]
        public virtual Date ExpirationDate { get; set; }

    }
}
