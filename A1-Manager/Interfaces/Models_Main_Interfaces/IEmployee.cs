﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Main_Interfaces
{
    public interface IEmployee
    {
        public int Id { get; set; }

        public string ImageURL { get; set; }

        public string Status { get; set; }

        public int LeaveRemaining { get; set; }
    }
}
