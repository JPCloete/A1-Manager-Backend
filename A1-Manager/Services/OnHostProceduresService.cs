using A1_Manager.ApplicationDbContext;
using A1_Manager.Models_Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace A1_Manager.Services
{
    //Service used to get a list of currencies and countries to store in application's database.
    public class Currency_LocationService : Controller
    {
        private readonly AppDbContext _db;
        private readonly HttpClient _client;

        public Currency_LocationService(AppDbContext db, HttpClient client)
        {

            _client = client;

            _db = db;
        }

        public class CurrenciesClass
        {
            public string Success { get; set; }

            public string Terms { get; set; }

            public string Privacy { get; set; }

            public Dictionary<string, string> Currencies { get; set;}
        }

        public class CountryClass
        {
            public ICollection<CountryData> Data { get; set; }
        }

        public class CountryData
        {
            public string Country { get; set; }
        }

        //This endpoint is only used for deployment
        [HttpGet]
        [Route("/add-currencies")]
        public async Task<string> StoreCurrenciesAsync()
        {

            var dbCurrencies = await _db.Currencies
                .ToListAsync();
            
                if(dbCurrencies.Count != 0)
            {
                return "Invalid Request";
            }

            var currencyResponseMessage = await _client.GetAsync("http://api.currencylayer.com/list?access_key=a9e7a42c970152a1e18befa4bdce36d5");
            var currencyResponse = await currencyResponseMessage.Content.ReadAsStringAsync();
            CurrenciesClass currenciesClass = JsonConvert.DeserializeObject<CurrenciesClass>(currencyResponse);
            var currencies = currenciesClass.Currencies;

            foreach (var currency in currencies)
            {
                Currency currencyEntry = new Currency()
                {
                    Name = currency.Value,
                    Symbol = currency.Key,
                };

                await _db.Currencies.AddAsync(currencyEntry);
            }

            await _db.SaveChangesAsync();

            return "Success";
        }

        //This endpoint is only used for deployment
        [HttpGet]
        [Route("/add-countries")]
        public async Task<string> StoreCountriesAsync()
        {

            var dbCountries = await _db.Countries
                .ToListAsync();

            if (dbCountries.Count != 0)
            {
                return "Invalid Request";
            }

            var offset = 0;
            var countryCount = 251;
            while (offset < countryCount) {
                var countryResponseMessage = await _client.GetAsync("https://api.first.org/data/v1/countries?fields=country&envelope=false&offset=" + offset.ToString());
                var countryResponse = await countryResponseMessage.Content.ReadAsStringAsync();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                dynamic countries = jss.Deserialize<object>(countryResponse);
                foreach (var country in countries)
                {

                    Country countryEntry = new Country()
                    {
                        Name = country.Value.Country,
                    };

                    await _db.Countries.AddAsync(countryEntry);
                }
                //api allows max offset of 100
                offset += 100;
            }

            await _db.SaveChangesAsync();

            return "Success";
        }
    }
}
