using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Services_Interfaces
{
    public interface IContractService
    {
        Task<int> AddContractAsync(string pdfUrl, string signedDate, string expirationDate);

        Task<bool> DeleteContractAsync(int? id);
    }
}
