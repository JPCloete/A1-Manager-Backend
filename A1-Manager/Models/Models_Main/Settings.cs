using A1_Manager.Models_Main;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models.Models_Main
{
    public class Settings
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public bool AutoClockOutEnabled { get; set; }

        public int? AutoClockOutTimeId { get; set; }

        [ForeignKey("AutoClockOutTimeId")]
        public Date? AutoClockOutTime { get; set; }

        public bool SupplierContractRequired { get; set; }
        
        public bool EmployeeContractRequired { get; set; }

        public bool BranchProductRequestRequired { get; set; }

        public bool RoleRequestRequired { get; set; }
    }
}
