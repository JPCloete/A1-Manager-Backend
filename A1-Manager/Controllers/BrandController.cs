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
    public class BrandController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ISerializationService _serialization;
        private readonly IDateService _date;
        private readonly IMoneyService _money;

        public BrandController(AppDbContext db, ISerializationService serialization, IDateService date, IMoneyService money)
        {
            _db = db;
            _serialization = serialization;
            _date = date;
            _money = money;
        }

        [HttpPost]
        [Route("/brand")]
        public async Task<string> AddBrandAsync([FromBody] Brand brand)
        {
            if(string.IsNullOrEmpty(brand.Name) || string.IsNullOrEmpty(brand.Telephone) ||
                string.IsNullOrEmpty(brand.Email) || string.IsNullOrEmpty(brand.PreferredCurrency.Symbol))
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            int prefferedCurrencyId = await _money.VerifyCurrencyAsync(brand.PreferredCurrency.Symbol);
            brand.PreferredCurrency = null; //set to null to avoid another unwanted entry
            if(prefferedCurrencyId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Currency");
            }

            brand.PreferredCurrencyId = prefferedCurrencyId;
           
            int dateId = await _date.AddDateAsync(DateTime.UtcNow.ToString("yyyy/MM/dd"));
            if(dateId == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Date");
            }

            brand.DateAddedId = dateId;

            var isDuplicateBrand = await _db.Brands
                .Where(x => x.Name == brand.Name)
                .FirstOrDefaultAsync();

            if(isDuplicateBrand != null)
            {
                return _serialization.SerializeMessage(401, "Brand Already Exists");
            }

            await _db.Brands.AddAsync(brand);
            await _db.SaveChangesAsync();

            var newlyAddedBrand = await _db.Brands
                .Where(x => x.Id == brand.Id)
                .Select(y => new
                {
                    y.Id,
                    y.Name,
                    y.LogoURL
                })
                .FirstOrDefaultAsync();

            return "Success";
        }

        [HttpGet]
        [Route("/brand")]
        public async Task<string> GetBrandAsync([FromQuery]int id)
        {
            if(id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var brand = await _db.Brands
                .Where(x => x.Id == id)
                .Include(y => y.DateAdded)
                .Include(x => x.PreferredCurrency)
                .Select(y => new
                {
                    y.Id,
                    y.Name,
                    y.LogoURL,
                    y.PreferredCurrency.Symbol,
                    y.Email,
                    y.Telephone,
                    DateAdded = y.DateAdded.Time
                })
                .FirstOrDefaultAsync();

            if (brand != null)
            {
                return _serialization.SerializeObject(brand);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpGet]
        [Route("/brands")]
        public async Task<string> GetBrandsAsync([FromQuery] int offSet)
        {
            if(offSet < 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var brands = await _db.Brands
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.LogoURL
                })
                .Skip(offSet * 20)
                .Take(20)
                .ToListAsync();

            if(brands != null)
            {
                return _serialization.SerializeObject(brands);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpPut]
        [Route("/brand")]
        public async Task<string> UpdateBrandAsync([FromBody] Brand brand)
        {
            if (string.IsNullOrEmpty(brand.Name) || string.IsNullOrEmpty(brand.Telephone) ||
                string.IsNullOrEmpty(brand.Email) || string.IsNullOrEmpty(brand.PreferredCurrency.Symbol) ||
                brand.Id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            var originalBrand = await _db.Brands
                .Where(x => x.Id == brand.Id)
                .FirstOrDefaultAsync();

            if(originalBrand != null)
            {
                brand.PreferredCurrencyId = originalBrand.PreferredCurrencyId; //PreferredCurrency can't be changed, it will cascade throughout the brand.
                brand.DateAddedId = originalBrand.DateAddedId; //DateAdded should stay consistent after update.

                var doesBrandExist = await _db.Brands
                    .Where(x => x.Name == brand.Name)
                    .FirstOrDefaultAsync();

                if (doesBrandExist != null)
                {
                    return _serialization.SerializeMessage(401, "Duplicate Brand Name");
                }

                _db.Entry(originalBrand).State = EntityState.Detached;
                _db.Entry(brand).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                var updatedBrand = await _db.Brands
                    .Where(x => x.Id == brand.Id)
                    .Select(y => new
                    {
                        y.Id,
                        y.Name,
                        y.LogoURL,
                        y.PreferredCurrency.Symbol,
                        y.Email,
                        y.Telephone,
                        DateAdded = y.DateAdded.Time
                    })
                    .FirstOrDefaultAsync();

                return _serialization.SerializeObject(updatedBrand);
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }

        [HttpDelete]
        [Route("/brand")]
        public async Task<string> DeleteBrandAsync([FromQuery] int id)
        {
            if (id == 0)
            {
                return _serialization.SerializeMessage(404, "Invalid Request");
            }

            Brand brand = await _db.Brands
                 .Where(x => x.Id == id)
                 .FirstOrDefaultAsync();

            if(brand != null) { 
                _db.Brands.Remove(brand);
                await _db.SaveChangesAsync();

                return "Success";
            }

            return _serialization.SerializeMessage(404, "Not Found");
        }
    }
}
