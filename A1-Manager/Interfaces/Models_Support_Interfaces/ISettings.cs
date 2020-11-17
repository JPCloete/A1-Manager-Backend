using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Models_Support_Interfaces
{
    public interface ISettings
    {
        public int Id { get; set; }

        public bool AutoClockOutEnabled { get; set; }

        public bool SupplierContractRequired { get; set; }

        public bool EmployeeContractRequired { get; set; }

        public bool BranchProductRequestRequired { get; set; }

        public bool RoleRequestRequired { get; set; }
    }
}
