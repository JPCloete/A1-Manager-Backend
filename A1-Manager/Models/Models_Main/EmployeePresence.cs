using A1_Manager.Models_Main;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models.Models_Main
{
    public class EmployeePresence
    {
        public virtual int Id { get; set; }

        public virtual int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual int ClockInTimeId { get; set; }

        [ForeignKey("ClockInTimeId")]
        public virtual Date ClockInTime { get; set; }

        public virtual int ClockOutTimeId { get; set; }

        [ForeignKey("ClockOutTimeId")]
        public virtual Date ClockOutTime { get; set; }
    }
}
