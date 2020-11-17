using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Services_Interfaces
{
    public interface IDateService 
    {
        Task<int> AddDateAsync(string dateTime);

        bool VerifyValidDate(string dateString, bool isAfterCurrentDate);

        bool VerifyValidHourlyDate(string dateString, bool isAfterCurrentDate);
    }
}
