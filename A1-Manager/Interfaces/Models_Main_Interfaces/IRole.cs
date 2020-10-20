using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Main_Interfaces
{
    public interface IRole
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
