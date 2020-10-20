using A1_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Support
{
    public class Identity : IIdentity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
