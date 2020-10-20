using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.ModelInterfaces
{
    public interface ICurrency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }
    }
}
