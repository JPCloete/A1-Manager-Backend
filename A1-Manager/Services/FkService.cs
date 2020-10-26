using A1_Manager.ApplicationDbContext;
using A1_Manager.Interfaces.Services_Interfaces;
using A1_Manager.Models_Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Services
{
    public class FkService : IFkService
    {
        private readonly AppDbContext _db;
        public FkService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> VerifyBrandAsync(int brandId)
        {
            if(brandId == 0)
            {
                return false;
            }

            Brand brand = await _db.Brands
                .Where(x => x.Id == brandId)
                .FirstOrDefaultAsync();

            if(brand != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> VerifyBranchAsync(int branchId)
        {
            if(branchId == 0)
            {
                return false;
            }

            Branch branch = await _db.Branches
                .Where(x => x.Id == branchId)
                .FirstOrDefaultAsync();

            if(branch != null)
            {
                return true;
            }

            return false; 
        }

        public async Task<bool> VerifyEmployeeAsync(int employeeId)
        {
            if(employeeId == 0)
            {
                return false;
            }

            Employee employee = await _db.Employees
                .Where(x => x.Id == employeeId)
                .FirstOrDefaultAsync();

            if(employee != null)
            {
                return true;
            }

            return false;
        }
    }
}
