using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A1_Manager.ApplicationDbContext;
using A1_Manager.Interfaces.Services_Interfaces;
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
    }
}
