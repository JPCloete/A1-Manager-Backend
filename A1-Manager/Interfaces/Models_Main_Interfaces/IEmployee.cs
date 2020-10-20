using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces
{
    public interface IEmployee
    {
        public int Id { get; set; }

        public string ImageURL { get; set; }

        public int Presence { get; set; }

        public int LeaveRemaining { get; set; }
    }
}
