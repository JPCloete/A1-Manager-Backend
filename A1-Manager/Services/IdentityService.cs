using A1_Manager.ApplicationDbContext;
using A1_Manager.Interfaces.Services_Interfaces;
using A1_Manager.Models_Support;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Services
{
    /*
    Service used to reduce redundant name data eg.
    2 employees having the same First Name will both have a foreign key referring to the same row in the Identity Table
    */
    public class IdentityService : IIdentityService
    {
        private readonly AppDbContext _db;
        public IdentityService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<int> AddIdentityAsync(string identityName)
        {
            if(string.IsNullOrEmpty(identityName))
            {
                return 0;
            }

            Identity identity = await _db.Identities
                .Where(x => x.Name.ToLower() == identityName.ToLower())
                .FirstOrDefaultAsync();

            if(identity != null)
            {
                return identity.Id;
            }

            var firstCharUppercaseString = char.ToUpper(identityName[0]) + identityName.Substring(1).ToLower();
            Identity identityEntry = new Identity()
            {
                Name = firstCharUppercaseString
            };

            await _db.Identities.AddAsync(identityEntry);
            await _db.SaveChangesAsync();

            return identityEntry.Id;
        }
    }
}
