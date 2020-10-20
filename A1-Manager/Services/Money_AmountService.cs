using A1_Manager.ApplicationDbContext;
using A1_Manager.Models_Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Services
{
    public class Money_AmountService : Controller
    {
        private readonly AppDbContext _db;

        public Money_AmountService(AppDbContext db)
        {
            _db = db;
        }

        public virtual async Task<(int, string)> AddMoneyAsync(float moneyAmount, string currencySymbol)
        {
            (Money, Currency, string) moneyResult = await GetMoneyAsync(moneyAmount, currencySymbol);
            if (moneyResult.Item3 == "Invalid Request" || moneyResult.Item3 == "Invalid Currency Type")
            {
                return (0, "Invalid Request");
            }
            else if (moneyResult.Item3 == "No entry")
            {
                Money money = new Money()
                {
                    Amount = moneyAmount,
                    CurrencyId = moneyResult.Item2.Id
                };

                await _db.Money.AddAsync(money);
                await _db.SaveChangesAsync();

                return (money.Id, "Success");
            }
            else
            {
                return (moneyResult.Item1.Id, "Success");
            }
        }

        private async Task<(Money, Currency, string)> GetMoneyAsync(float moneyAmount, string currencySymbol)
        {
            if(moneyAmount == 0 || string.IsNullOrEmpty(currencySymbol))
            {
                return (null, null, "Invalid Request");
            }

            (Currency, bool) isValidCurrency = await VerifyCurrencyAsync(currencySymbol);
            if (isValidCurrency.Item2 == false)
            {
                return (null, null, "Invalid Currency Type");
            }

            var money = await _db.Money
                .Where(x => x.Amount == moneyAmount)
                .Where(y => y.Currency.Symbol.ToLower() == currencySymbol.ToLower())
                .FirstOrDefaultAsync();

            if(money != null)
            {
                return (money, isValidCurrency.Item1,"Success");
            }
            else
            {
                return (null, isValidCurrency.Item1, "No entry");
            }
        }

        private async Task<(Currency, bool)> VerifyCurrencyAsync(string currencySymbol)
        {
            if (string.IsNullOrEmpty(currencySymbol))
            {
                return (null, false);
            }

            var currency = await _db.Currencies
                .Where(x => x.Symbol == currencySymbol)
                .FirstOrDefaultAsync();

            if(currency != null)
            {
                return (currency, true);
            } 
            else
            {
                return (null, false);
            }
        }
    }
}
