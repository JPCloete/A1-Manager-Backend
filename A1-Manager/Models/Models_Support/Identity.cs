using A1_Manager.Support_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Support
{
    public class Identity : IIdentity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
    }
}
