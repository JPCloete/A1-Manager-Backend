using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Services_Interfaces
{
    public interface ILocationService
    {
        Task<int> VerifyCountryAsync(string countryName);

        Task<int> AddCityAsync(string cityName);
    }
}
