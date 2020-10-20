using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces
{
    public interface IOrder
    {
        public int Id { get; set; }

        public int OrderStatus { get; set; }
    }
}
