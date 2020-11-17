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
    public class DateService : IDateService
    {
        private readonly AppDbContext _db;

        public DateService(AppDbContext db)
        {
            _db = db;

        }

        public async Task<int> AddDateAsync(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
            {
                return 0;
            }

            var date = await _db.Dates
                .Where(x => x.Time == dateTime)
                .FirstOrDefaultAsync();

            if (date != null)
            {
                return date.Id;
            }

            Date dateEntry = new Date()
            {
                Time = dateTime
            };

            await _db.Dates.AddAsync(dateEntry);
            await _db.SaveChangesAsync();

            return dateEntry.Id;
        }

        public bool VerifyValidDate(string dateString, bool isAfterCurrentDate)
        {
            DateTime? isValidDateTime;
            try //try catch used for exception handling when dateString can't be parsed to DateTime type.
            {
                isValidDateTime = DateTime.ParseExact(dateString, "yyyy/MM/dd", null);
            }
            catch
            {
                return false;
            }
            if (isValidDateTime is DateTime)
            {
                if (isAfterCurrentDate == true)
                {
                    if (DateTime.UtcNow < isValidDateTime)
                    {
                        return true;
                    }

                    return false;
                }

                if (DateTime.UtcNow >= isValidDateTime)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool VerifyValidHourlyDate(string dateString, bool isAfterCurrentDate)
        {
            DateTime? isValidDateTime;
            try //try catch used for exception handling when dateString can't be parsed to DateTime type.
            {
                isValidDateTime = DateTime.ParseExact(dateString, "yyyy/MM/dd-HH/mm", null);
            }
            catch
            {
                return false;
            }
            if (isValidDateTime is DateTime)
            {
                if (isAfterCurrentDate == true)
                {
                    if (DateTime.UtcNow < isValidDateTime)
                    {
                        return true;
                    }

                    return false;
                }

                if (DateTime.UtcNow >= isValidDateTime)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
