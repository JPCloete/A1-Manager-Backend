using A1_Manager.Models_Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Joins
{
    public class EmployeeRole
    {
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

    }
}
