using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Services_Interfaces
{
    public interface IFkService
    {
        Task<bool> VerifyBrandAsync(int brandId);

        Task<bool> VerifyBranchAsync(int branchId);

        Task<bool> VerifyEmployeeAsync(int employeeId);
    }
}
