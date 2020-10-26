using A1_Manager.ApplicationDbContext;
using A1_Manager.Interfaces.Services_Interfaces;
using A1_Manager.Models_Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Services
{
    public class MoneyService : IMoneyService
    {
        private readonly AppDbContext _db;

        public MoneyService(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("/add-money")]
        //Checks if money exists in database and returns moneyId. If money doesn't exists adds it to database and returns moneyId
        public virtual async Task<int> AddMoneyAsync([FromQuery] float moneyAmount, [FromQuery] string currencySymbol)
        {
            (int, int, int) moneyResult = await GetMoneyAsync(moneyAmount, currencySymbol);
            if (moneyResult.Item3 == 0) //0 = Invalid request
            {
                return 0;
            }
            else if (moneyResult.Item3 == 1) //1 = Valid request but money entry doesn't exist
            {
                Money money = new Money()
                {
                    Amount = moneyAmount,
                    CurrencyId = moneyResult.Item2
                };

                await _db.Money.AddAsync(money);
                await _db.SaveChangesAsync();

                return (money.Id);
            }
            //moneyResult.Item3 == 2 in this case, 2 = Valid request and a matching money row(matching currency Type and money Amount) exists in the database
            return (moneyResult.Item1);
        }

        /*
        GetMoneyAsync has a Tuple return type Tuple[0] = moneyId; Tuple[1] = currencyId; 
        Tuple[2] = functionStatus(if functionStatus == 0 { Invalid Request }; if functionStatus == 1 { Valid request but money entry doesn't exist };
        if functionStatus == 2 { Valid request and a matching money row(matching currency Type and money Amount) exists in the database }
        */
        public async Task<(int, int, int)> GetMoneyAsync(float moneyAmount, string currencySymbol)
        {
            if(string.IsNullOrEmpty(currencySymbol))
            {
                return (0, 0, 0);
            }

            int currencyId = await VerifyCurrencyAsync(currencySymbol);
            if (currencyId == 0)
            {
                return (0, 0, 0);
            }

            var money = await _db.Money
                .Where(x => x.Amount == moneyAmount)
                .Where(y => y.Currency.Symbol.ToLower() == currencySymbol.ToLower())
                .FirstOrDefaultAsync();

            if(money != null)
            {
                return (money.Id, currencyId, 2);
            }

            return (0, currencyId , 1);
        }

        public async Task<int> VerifyCurrencyAsync(string currencySymbol) //returns currencyId if currency Symbol is valid, if invalid returns 0
        {
            if (string.IsNullOrEmpty(currencySymbol))
            {
                return 0;
            }

            var currency = await _db.Currencies
                .Where(x => x.Symbol == currencySymbol)
                .FirstOrDefaultAsync();

            if(currency != null)
            {
                return currency.Id;
            } 

            return 0;
        }
    }
}
