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
    public class AmountService : IAmountService
    {
        public readonly AppDbContext _db;

        public AmountService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddAmountAsync(float amountVolume, string amountVolumeType)
        {
            if (amountVolume < 0 ||
                (amountVolumeType.ToLower() != "kilogram" && amountVolumeType.ToLower() != "ton" &&
                amountVolumeType.ToLower() != "gram" && amountVolumeType.ToLower() != "unit"))
            {
                return 0;
            }

            Amount amount = await _db.Amount
                .Where(x => x.Volume == amountVolume)
                .Where(y => y.VolumeType.ToLower() == amountVolumeType.ToLower())
                .FirstOrDefaultAsync();

            if(amount != null)
            {
                return amount.Id;
            }

            Amount amountEntry = new Amount()
            {
                Volume = amountVolume,
                VolumeType = amountVolumeType
            };

            await _db.Amount.AddAsync(amountEntry);
            await _db.SaveChangesAsync();

            return amountEntry.Id;
        }
    }
}
