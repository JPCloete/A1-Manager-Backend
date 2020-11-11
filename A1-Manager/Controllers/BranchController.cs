using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A1_Manager.ApplicationDbContext;
using A1_Manager.Interfaces.Services_Interfaces;
using A1_Manager.Models_Main;
using A1_Manager.Models_Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResponseSerialization;

namespace A1_Manager.Controllers
{
    public class BranchController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ISerializationService _serialization;
        private readonly ILocationService _location;
        private readonly IMoneyService _money;
        private readonly IDateService _date;
        private readonly IIdentityService _identity;
        private readonly IFkService _fkey;

        public BranchController(AppDbContext db, ISerializationService serialization, 
            ILocationService location, IMoneyService money, IDateService date
            , IIdentityService identity, IFkService fkey)
        {
            _db = db;
            _serialization = serialization;
            _location = location;
            _money = money;
            _date = date;
            _identity = identity;
            _fkey = fkey;
        }

        [HttpPost]
        [Route("/branch")]
        public async Task<string> AddBranchAsync([FromBody] Branch branch)
        {
            if (string.IsNullOrEmpty(branch.Name.Name) || string.IsNullOrEmpty(branch.City.Name) ||
                string.IsNullOrEmpty(branch.Email) || string.IsNullOrEmpty(branch.PreferredCurrency.Symbol) ||
                string.IsNullOrEmpty(branch.Address) || string.IsNullOrEmpty(branch.Telephone) ||
                branch.OccupancyCost.Amount < 0 || string.IsNullOrEmpty(branch.Country.Name) ||
                branch.BrandId == 0)//Checks if all required properties are present
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var isValidBrand = await _fkey.VerifyBrandAsync(branch.BrandId);
            if(isValidBrand == false)
            {
                return _serialization.SerializeMessage(404, "Invalid Brand");
            }

            int nameId = await _identity.AddIdentityAsync(branch.Name.Name);
            if(nameId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Name");
            }

            branch.NameId = nameId;
            branch.Name = null; //set to null to avoid another unwanted entry

            int countryId = await _location.VerifyCountryAsync(branch.Country.Name);
            if (countryId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Country");
            }

            branch.CountryId = countryId;
            branch.Country = null; //set to null to avoid another unwanted entry

            int cityId = await _location.AddCityAsync(branch.City.Name);
            if (cityId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            branch.CityId = cityId;
            branch.City = null; //set to null to avoid another unwanted entry

            int currencyId = await _money.VerifyCurrencyAsync(branch.PreferredCurrency.Symbol);
            if (currencyId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Currency");
            }

            branch.PreferredCurrencyId = currencyId;

            int occupancyCostId = await _money.AddMoneyAsync(branch.OccupancyCost.Amount, branch.PreferredCurrency.Symbol);
            if (occupancyCostId == 0)
            {
                return "Invalid Request";
            }

            branch.OccupancyCostId = occupancyCostId;
            branch.PreferredCurrency = null; //can only be set null after occupancyCostId is found, it is a argument in AddMoneyAsync, set to null to avoid another unwanted entry
            branch.OccupancyCost = null; //set to null to avoid another unwanted entry

            int dateId = await _date.AddDateAsync(DateTime.UtcNow.ToString("yyyy/MM/dd"));
            if (dateId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Date");
            }

            branch.DateAddedId = dateId;

            var isDuplicateBranch = await _db.Branches
                .Where(x => x.NameId == branch.NameId)
                .Where(x => x.CountryId == branch.CountryId)
                .Where(y => y.CityId == branch.CityId)
                .Where(x => x.Address == branch.Address)
                .FirstOrDefaultAsync();

            if (isDuplicateBranch != null)
            {
                return _serialization.SerializeMessage(500, "Duplicate Branch");
            }

            await _db.Branches.AddAsync(branch);
            await _db.SaveChangesAsync();

            var newlyAddedBranch = await _db.Branches
                .Where(x => x.Id == branch.Id)
                .Include(y => y.Country)
                .Include(x => x.City)
                .Select(x => new
                {
                    x.Id,
                    x.Name.Name,
                    Country = x.Country.Name,
                    City = x.City.Name,
                    x.Address
                })
                .FirstOrDefaultAsync();

            return _serialization.SerializeObject(newlyAddedBranch);
        }

        [HttpGet]
        [Route("/branch")]
        public async Task<string> GetBranchAsync([FromQuery] int id)
        {
            if (id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var branch = await _db.Branches
                .Where(x => x.Id == id)
                .Include(y => y.Country)
                .Include(x => x.City)
                .Include(y => y.Name)
                .Select(y => new
                {
                    y.Id,
                    y.Name.Name,
                    y.Email,
                    y.Telephone,
                    DateAdded = y.DateAdded.Time,
                    Location = new
                    {
                        Country = y.Country.Name,
                        City = y.City.Name,
                        y.Address
                    },
                    OccupancyCost = new 
                    {
                        Currency = y.PreferredCurrency.Symbol,
                        Cost = y.OccupancyCost.Amount
                    }
                })
                .FirstOrDefaultAsync();

            if (branch != null)
            {
                return _serialization.SerializeObject(branch);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpGet]
        [Route("/branches")]
        public async Task<string> GetBranchesAsync([FromQuery] int brandId, [FromQuery] int offSet)
        {
            if (brandId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var branches = await _db.Branches
                .Where(x => x.BrandId == brandId)
                .Include(y => y.Country)
                .Include(x => x.City)
                .Select(x => new
                {
                    x.Id,
                    x.Name.Name,
                    Country = x.Country.Name,
                    City = x.City.Name,
                    x.Address
                })
                .Skip(offSet * 20)
                .Take(20)
                .ToListAsync();
            
            if(branches != null)
            {
                return _serialization.SerializeObject(branches);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpPut]
        [Route("/branch")]
        public async Task<string> UpdateBranchAsync([FromBody] Branch branch)
        {
            if (branch.Id == 0 || string.IsNullOrEmpty(branch.Name.Name) ||
                string.IsNullOrEmpty(branch.City.Name) || string.IsNullOrEmpty(branch.Email) || 
                string.IsNullOrEmpty(branch.PreferredCurrency.Symbol) || string.IsNullOrEmpty(branch.Telephone) ||
                string.IsNullOrEmpty(branch.Country.Name) || string.IsNullOrEmpty(branch.Address) ||
                branch.BrandId == 0 || branch.OccupancyCost.Amount < 0)//Checks if all required properties are present
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var originalBranch = await _db.Branches //Query to get original values to simplify abstract post request properties
                .Where(x => x.Id == branch.Id)
                .FirstOrDefaultAsync();

            if (originalBranch != null)
            {
                int nameId = await _identity.AddIdentityAsync(branch.Name.Name);
                if (nameId == 0)
                {
                    return _serialization.SerializeMessage(404, "Invalid Name");
                }

                branch.NameId = nameId;
                branch.Name = null; //set to null to avoid another unwanted entry

                int countryId = await _location.VerifyCountryAsync(branch.Country.Name);
                if (countryId == 0)
                {
                    return _serialization.SerializeMessage(404, "Invalid Country");
                }

                branch.CountryId = countryId;
                branch.Country = null; //set to null to avoid another unwanted entry

                int cityId = await _location.AddCityAsync(branch.City.Name);
                if (cityId == 0)
                {
                    return _serialization.SerializeMessage(404, "Invalid Request");
                }

                branch.CityId = cityId;
                branch.City = null; //set to null to avoid another unwanted entry

                int currencyId = await _money.VerifyCurrencyAsync(branch.PreferredCurrency.Symbol);
                if (currencyId == 0)
                {
                    return _serialization.SerializeMessage(404, "Invalid Currency");
                }

                branch.PreferredCurrencyId = currencyId;

                int occupancyCostId = await _money.AddMoneyAsync(branch.OccupancyCost.Amount, branch.PreferredCurrency.Symbol);
                if (occupancyCostId == 0)
                {
                    return "Invalid Request";
                }

                branch.OccupancyCostId = occupancyCostId;
                branch.OccupancyCost = null; //set to null to avoid another unwanted entry
                branch.PreferredCurrency = null;//can only be set null after occupancyCostId is found, it's an argument in AddMoneyAsync.  

                branch.Id = originalBranch.Id;
                branch.DateAddedId = originalBranch.DateAddedId;
                branch.BrandId = originalBranch.BrandId;

                var isDuplicateBranch = await _db.Branches
                    .Where(x => x.NameId == branch.NameId)
                    .Where(x => x.CountryId == branch.CountryId)
                    .Where(y => y.CityId == branch.CityId)
                    .Where(x => x.Address == branch.Address)
                    .FirstOrDefaultAsync();

                if (isDuplicateBranch != null)
                {
                    return _serialization.SerializeMessage(500, "Duplicate Branch");
                }

                _db.Entry(originalBranch).State = EntityState.Detached;
                _db.Entry(branch).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return "Success";
            }
            else
            {
                return _serialization.SerializeMessage(404, "Not Found");
            }
        }

        [HttpDelete]
        [Route("/branch")]
        public async Task<string> DeleteBranchAsync([FromQuery] int id)
        {
            if(id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            Branch branch = await _db.Branches
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            _db.Branches.Remove(branch);
            await _db.SaveChangesAsync();

            return "Success";
        }
    }
}