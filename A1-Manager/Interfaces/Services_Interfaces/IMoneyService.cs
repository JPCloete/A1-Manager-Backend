using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Services_Interfaces
{
    public interface IMoneyService
    {
        Task<int> AddMoneyAsync(float moneyAmount, string currencySymbol);

        Task<(int, int, int)> GetMoneyAsync(float moneyAmount, string currencySymbol);

        Task<int> VerifyCurrencyAsync(string currencySymbol);
    }
}
