using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces
{
    public interface IAmount
    {
        public int Id { get; set; }

        public float Volume { get; set; }

        public string VolumeType { get; set; }
    }
}
