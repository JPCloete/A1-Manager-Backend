using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces
{
    public interface IMoney
    {
        public int Id { get; set; }

        public float Amount { get; set; }
    }
}
