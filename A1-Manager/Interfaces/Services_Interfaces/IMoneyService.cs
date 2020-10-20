using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Services_Interfaces
{
    interface IMoneyService
    {
        Task<(int, string)> AddMoneyAsync(float moneyAmount, string currencySymbol);


    }
}
