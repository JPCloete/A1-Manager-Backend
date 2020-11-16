using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A1_Manager.ApplicationDbContext;
using A1_Manager.Interfaces.Services_Interfaces;
using A1_Manager.Models_Joins;
using A1_Manager.Models_Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResponseSerialization;

namespace A1_Manager.Controllers
{
    public class RoleController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ISerializationService _serialization;
        private readonly IIdentityService _identity;
        private readonly IFkService _fkey;
        public RoleController(AppDbContext db, ISerializationService serialization, IIdentityService identity, IFkService fkey)
        {
            _db = db;
            _serialization = serialization;
            _identity = identity;
            _fkey = fkey;
        }

        [HttpPost]
        [Route("/role")]    
        public async Task<dynamic> AddRoleAsync([FromBody] Role role)
        {
            if(string.IsNullOrEmpty(role.Name.Name) || role.BrandId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            Role isDuplicateRole = await _db.Roles
                .Where(x => x.Name.Name.ToLower() == role.Name.Name.ToLower())
                .FirstOrDefaultAsync();

            if(isDuplicateRole != null)
            {
                return _serialization.SerializeMessage(403, "Role Already Exists");
            }

            bool isValidBrand = await _fkey.VerifyBrandAsync(role.BrandId);
            if(isValidBrand == false)
            {
                return _serialization.SerializeMessage(404, "Invalid Brand");
            }

            int nameId = await _identity.AddIdentityAsync(role.Name.Name);
            if(nameId == 0)
            {
                return _serialization.SerializeMessage(403, "Invalid Role Name");
            }

            role.NameId = nameId;
            role.Name = null;

            await _db.AddAsync(role);
            await _db.SaveChangesAsync();

            var newlyAddedRole = await _db.Roles
                .Where(x => x.Id == role.Id)
                .Include(y => y.Name)
                .Select(x => new
                {
                    x.Id,
                    x.Name.Name,
                    x.Description
                })
                .FirstOrDefaultAsync();

            return _serialization.SerializeObject(newlyAddedRole);
        }

        [HttpGet]
        [Route("/role")]
        public async Task<string> GetRoleAsync(int id)
        {
            if (id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var role = await _db.Roles
                .Where(x => x.Id == id)
                .Select(y => new
                {
                    y.Id,
                    y.Name.Name,
                    y.Description,
                    Brand = y.Brand.Name
                })
                .FirstOrDefaultAsync();

            if (role != null)
            {
                return _serialization.SerializeObject(role);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpGet]
        [Route("/roles")]
        public async Task<string> GetRolesAsync([FromQuery] int brandId, [FromBody] int offSet)
        {
            if(brandId == 0 || offSet < 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var roles = await _db.Roles
                .Where(x => x.BrandId == brandId)
                .Select(y => new
                {
                    y.Id,
                    y.Name.Name
                })
                .ToListAsync();

            if(roles != null)
            {
                return _serialization.SerializeObject(roles);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpDelete]
        [Route("/role")]
        public async Task<string> DeleteRoleAsync([FromQuery] int id) 
        {
            if(id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            Role role = await _db.Roles
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if(role != null)
            {
                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();

                return "Success";
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpPost]
        [Route("/employee-roles")]
        public async Task<string> AddEmployeeRoles([FromQuery] int employeeId, [FromBody] ICollection<int> ids)
        {
            if(ids.Contains(0) || ids.Count == 0 || employeeId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            Employee employee = await _db.Employees
                .Where(x => x.Id == employeeId)
                .FirstOrDefaultAsync();

            if (employee != null)
            {
                EmployeeRole employeeRole = new EmployeeRole(); //Reuse one object instance to avoid unnecessary memory usage/ memory leak
                ICollection<dynamic> employeeRoles = new List<dynamic>(); //ICollection object used to return new roles added to specific employee

                foreach (var id in ids)
                {
                    var isValidRole = await _db.Roles
                        .Where(x => x.Id == id)
                        .Select(y => new
                        {
                            y.Id,
                            y.Name.Name
                        })
                        .FirstOrDefaultAsync();

                    if (isValidRole != null)
                    {
                        employeeRole.EmployeeId = employeeId;
                        employeeRole.RoleId = id;

                        EmployeeRole hasRole = await _db.EmployeeRoles
                            .Where(x => x.EmployeeId == employeeId)
                            .Where(y => y.RoleId == id)
                            .FirstOrDefaultAsync();

                        if (hasRole != null)
                        {
                            //Do nothing
                        }
                        else
                        {
                            await _db.EmployeeRoles.AddAsync(employeeRole);
                            employeeRoles.Add(isValidRole);
                            await _db.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        return _serialization.SerializeMessage(404, "Not Found");
                    }
                   
                }

                return _serialization.SerializeObject(employeeRoles);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }
    }
}
