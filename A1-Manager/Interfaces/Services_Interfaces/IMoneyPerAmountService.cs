using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Services_Interfaces
{
    public interface IMoneyPerAmountService
    {
        Task<int> AddMoneyPerAmountAsync(float moneyAmount, string currencySymbol, float amountVolume, string amountVolumeType);
    }
}
