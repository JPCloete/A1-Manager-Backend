using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Services_Interfaces
{
    public interface IAmountService
    {
        Task<int> AddAmountAsync(float amountVolume, string amountVolumeType);
    }
}
