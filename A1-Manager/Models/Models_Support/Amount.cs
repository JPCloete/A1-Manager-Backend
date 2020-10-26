using A1_Manager.Support_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Support
{
    public class Amount : IAmount
    {
        public virtual int Id { get; set; }
        public virtual float Volume { get; set; }
        public virtual string VolumeType { get; set; }
    }
}
