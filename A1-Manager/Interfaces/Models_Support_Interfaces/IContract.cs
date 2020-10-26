using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Support_Interfaces
{
    public interface IContract
    {
        public int Id { get; set; }

        public string PdfURL { get; set; }
    }
}
