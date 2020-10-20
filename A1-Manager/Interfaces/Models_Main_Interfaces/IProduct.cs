using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Main_Interfaces
{
    public interface IProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public string BarCode { get; set; }

        public string BarCodeImageURL { get; set; }
    }
}
