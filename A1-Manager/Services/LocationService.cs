using A1_Manager.ApplicationDbContext;
using A1_Manager.Interfaces.Services_Interfaces;
using A1_Manager.Models_Support;
using Microsoft.EntityFrameworkCore;
using ResponseSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Services
{
    public class LocationService : ILocationService
    {
        private readonly AppDbContext _db;

        public LocationService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<int> VerifyCountryAsync(string countryName)
        {
            if(string.IsNullOrEmpty(countryName))
            {
                return 0;
            }

            var isValidCountry = await _db.Countries
                .Where(x => x.Name.ToLower() == countryName.ToLower())
                .FirstOrDefaultAsync();

            if(isValidCountry != null)
            {
                return isValidCountry.Id;
            }

            return 0;
        }

        public async Task<int> AddCityAsync(string cityName)
        {
            if(string.IsNullOrEmpty(cityName))
            {
                return 0;
            }

            var city = await _db.Cities
                .Where(x => x.Name.ToLower() == cityName.ToLower())
                .FirstOrDefaultAsync();

            if(city != null)
            {
                return city.Id;
            }

            City cityEntry = new City()
            {
                Name = cityName
            };

            await _db.Cities.AddAsync(cityEntry);
            await _db.SaveChangesAsync();

            return cityEntry.Id;
        }
    }
}
