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
    public class MoneyPerAmountService : IMoneyPerAmountService
    {
        private readonly AppDbContext _db;
        private readonly IMoneyService _money;
        private readonly IAmountService _amount;
        public MoneyPerAmountService(AppDbContext db, IMoneyService money, IAmountService amount)
        {
            _db = db;
            _money = money;
            _amount = amount;
        }

        public async Task<int> AddMoneyPerAmountAsync(float moneyAmount, string currencySymbol, float amountVolume, string amountVolumeType)
        {
            if(string.IsNullOrEmpty(currencySymbol) || string.IsNullOrEmpty(amountVolumeType) ||
               moneyAmount < 0 || amountVolume < 0)
            {
                return 0;
            }

            int moneyId = await _money.AddMoneyAsync(moneyAmount, currencySymbol);
            if(moneyId == 0)
            {
                return 0;
            }

            int amountId = await _amount.AddAmountAsync(amountVolume, amountVolumeType);
            if(amountId == 0)
            {
                return 0;
            }

            MoneyPerAmount moneyPerAmount = await _db.MoneyPerAmount
                .Where(x => x.MoneyId == moneyId)
                .Where(y => y.AmountId == amountId)
                .FirstOrDefaultAsync();

            if(moneyPerAmount != null)
            {
                return moneyPerAmount.Id;
            }

            MoneyPerAmount moneyPerAmountEntry = new MoneyPerAmount()
            {
                MoneyId = moneyId,
                AmountId = amountId
            };

            await _db.MoneyPerAmount.AddAsync(moneyPerAmountEntry);
            await _db.SaveChangesAsync();

            return moneyPerAmountEntry.Id;
        }
    }
}
 