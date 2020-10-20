using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces
{
    public interface IIdentity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
