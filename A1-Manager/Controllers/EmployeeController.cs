using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A1_Manager.ApplicationDbContext;
using A1_Manager.Interfaces.Services_Interfaces;
using A1_Manager.Models_Main;
using A1_Manager.Support_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResponseSerialization;

namespace A1_Manager.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ISerializationService _serialization;
        private readonly IContractService _contract;
        private readonly IIdentityService _identity;
        private readonly IMoneyService _money;
        private readonly IFkService _fkey;
        public EmployeeController(AppDbContext db, ISerializationService serialization, IContractService contract, IIdentityService identity, IMoneyService money, IFkService fkey)
        {
            _db = db;
            _serialization = serialization;
            _contract = contract;
            _identity = identity;
            _money = money;
            _fkey = fkey;
        }

        [HttpPost]
        [Route("/employee")]
        public async Task<string> AddEmployeeAsync([FromBody] Employee employee)
        {
            if (string.IsNullOrEmpty(employee.FirstName.Name) || string.IsNullOrEmpty(employee.LastName.Name) ||
                string.IsNullOrEmpty(employee.Salary.Currency.Symbol) || employee.Salary.Amount == 0 || 
                employee.BranchId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            bool isValidBranch = await _fkey.VerifyBranchAsync(employee.BranchId);
            if(isValidBranch == false)
            {
                return _serialization.SerializeMessage(404, "Invalid Branch");
            }

            int firstNameId = await _identity.AddIdentityAsync(employee.FirstName.Name);
            if(firstNameId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid FirstName");
            }

            employee.FirstNameId = firstNameId;
            employee.FirstName = null;

            int lastNameId = await _identity.AddIdentityAsync(employee.LastName.Name);
            if (lastNameId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid LastName");
            }

            employee.LastNameId = lastNameId;
            employee.LastName = null;

            int salaryId = await _money.AddMoneyAsync(employee.Salary.Amount, employee.Salary.Currency.Symbol);
            if(salaryId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Salary");
            }

            employee.SalaryId = salaryId;
            employee.Salary = null;

            //contract logic
            if (!string.IsNullOrEmpty(employee.Contract.PdfURL) && !string.IsNullOrEmpty(employee.Contract.SignedDate.Time) &&
                !string.IsNullOrEmpty(employee.Contract.ExpirationDate.Time)) //checks if contract should be added or not.
            {
                int contractId = await _contract.AddContractAsync(employee.Contract.PdfURL, employee.Contract.SignedDate.Time, employee.Contract.ExpirationDate.Time);
                if (contractId == 0)
                {
                    return _serialization.SerializeMessage(404, "Invalid Contract");
                }

                employee.ContractId = contractId;
                employee.Contract = null; //set to null to avoid another unwanted entry
            }

            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();

            var newlyAddedEmployee = await _db.Employees
                .Where(x => x.Id == employee.Id)
                .Include(y => y.FirstName)
                .Include(x => x.LastName)
                .Include(y => y.Salary)
                .Include(x => x.Branch)
                .Include(y => y.Roles)
                .Select(y => new {
                    y.Id,
                    FirstName = y.FirstName.Name,
                    LastName = y.LastName.Name,
                    y.Branch.Name.Name,
                    y.Status
                })
                .FirstOrDefaultAsync();

            return _serialization.SerializeObject(newlyAddedEmployee);
        }

        [HttpGet]
        [Route("/employee")]
        public async Task<string> GetEmployeeAsync([FromQuery] int id)
        {
            if(id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var Roles = await _db.EmployeeRoles
                .Where(x => x.EmployeeId == id)
                .Include(y => y.Role)
                .Select(x => new
                {
                    x.Role.Name.Name
                })
                .ToArrayAsync();

            var employee = await _db.Employees
                .Where(x => x.Id == id)
                .Include(y => y.FirstName)
                .Include(x => x.LastName)
                .Include(y => y.Salary)
                .Include(x => x.Branch)
                .Include(y => y.Roles)
                .Select(y => new { 
                    y.Id,
                    FirstName = y.FirstName.Name,
                    LastName = y.LastName.Name,
                    y.ImageURL,
                    y.Branch.Name.Name,
                    Roles,
                    Salary = new
                    {
                        y.Salary.Currency.Symbol,
                        y.Salary.Amount
                    },
                    y.Status
                })
                .FirstOrDefaultAsync();

            if (employee != null)
            {
                return _serialization.SerializeObject(employee);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpGet]
        [Route("/employees")]
        public async Task<string> GetBranchEmployeesAsync([FromQuery] int branchId, [FromQuery] int offSet)
        {
            if(branchId == 0 || offSet < 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var employees = await _db.Employees
                .Where(x => x.BranchId == branchId)
                .Select(y => new
                {
                    y.Id,
                    FirstName = y.FirstName.Name,
                    LastName = y.LastName.Name,
                    y.Branch.Name.Name,
                    y.Status
                })
                .Skip(offSet * 20)
                .Take(20)
                .ToListAsync();

            if(employees != null)
            {
                return _serialization.SerializeObject(employees);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }
        
        [HttpPut]
        [Route("/employee")]
        public async Task<string> UpdateEmployeeAsync([FromBody] Employee employee)
        {
            if (string.IsNullOrEmpty(employee.FirstName.Name) || string.IsNullOrEmpty(employee.LastName.Name) ||
                string.IsNullOrEmpty(employee.Salary.Currency.Symbol) || employee.Salary.Amount == 0 ||
                employee.BranchId == 0 || employee.Id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var originalEmployee = await _db.Employees
                .Where(x => x.Id == employee.Id)
                .Include(y => y.Contract)
                .FirstOrDefaultAsync();

            if(originalEmployee != null)
            {

                bool isValidBranch = await _fkey.VerifyBranchAsync(employee.BranchId);
                if (isValidBranch == false)
                {
                    return _serialization.SerializeMessage(404, "Invalid Branch");
                }

                int firstNameId = await _identity.AddIdentityAsync(employee.FirstName.Name);
                if (firstNameId == 0)
                {
                    return _serialization.SerializeMessage(404, "Invalid FirstName");
                }

                employee.FirstNameId = firstNameId;
                employee.FirstName = null;

                int lastNameId = await _identity.AddIdentityAsync(employee.LastName.Name);
                if (lastNameId == 0)
                {
                    return _serialization.SerializeMessage(404, "Invalid LastName");
                }

                employee.LastNameId = lastNameId;
                employee.LastName = null;

                int salaryId = await _money.AddMoneyAsync(employee.Salary.Amount, employee.Salary.Currency.Symbol);
                if (salaryId == 0)
                {
                    return _serialization.SerializeMessage(404, "Invalid Salary");
                }

                employee.SalaryId = salaryId;
                employee.Salary = null;


                if (employee.Contract.PdfURL != originalEmployee.Contract.PdfURL || employee.Contract.SignedDate.Time != originalEmployee.Contract.SignedDate.Time ||
                   employee.Contract.ExpirationDate.Time != originalEmployee.Contract.ExpirationDate.Time)
                {
                    int contractId = await _contract.AddContractAsync(employee.Contract.PdfURL, employee.Contract.SignedDate.Time, employee.Contract.ExpirationDate.Time);
                    if (contractId == 0)
                    {
                        return _serialization.SerializeMessage(404, "Invalid Contract");
                    }

                    employee.ContractId = contractId;
                    employee.Contract = null; //set to null to avoid another unwanted entry
                }

                _db.Entry(originalEmployee).State = EntityState.Detached;
                _db.Entry(employee).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                var Roles = await _db.EmployeeRoles
                .Where(x => x.EmployeeId == employee.Id)
                .Include(y => y.Role)
                .Select(x => new
                {
                    x.Role.Name.Name
                })
                .ToArrayAsync();

                var updatedEmployee = await _db.Employees
                    .Where(x => x.Id == employee.Id)
                    .Include(y => y.FirstName)
                    .Include(x => x.LastName)
                    .Include(y => y.Salary)
                    .Include(x => x.Branch)
                    .Include(y => y.Roles)
                    .Select(y => new {
                        y.Id,
                        FirstName = y.FirstName.Name,
                        LastName = y.LastName.Name,
                        y.ImageURL,
                        y.Branch.Name.Name,
                        Roles,
                        Salary = new
                        {
                            y.Salary.Currency.Symbol,
                            y.Salary.Amount
                        },
                        y.Status
                    })
                    .FirstOrDefaultAsync();

                return _serialization.SerializeObject(updatedEmployee);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }
    }
}
