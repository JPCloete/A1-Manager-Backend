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
    public class ContractService : IContractService
    {
        private readonly AppDbContext _db;
        private readonly IDateService _date;
        public ContractService(AppDbContext db, IDateService date)
        {
            _db = db;
            _date = date;
        }

        public async Task<int> AddContractAsync(string pdfUrl, string signedDate, string expirationDate)
        {
            if(string.IsNullOrEmpty(pdfUrl) || string.IsNullOrWhiteSpace(signedDate) ||
               string.IsNullOrEmpty(expirationDate))
            {
                return 0;
            }
           
            bool isValidSignedDate = _date.VerifyValidDate(signedDate, false); //checks if signedDate is valid and before or equal to current date  
            bool isExpirationDate = _date.VerifyValidDate(expirationDate, true); //checks if expirationDate is valid and after current date

            if (isValidSignedDate == false || isExpirationDate == false)
            {
                return 0;
            }

            int signedDateId = await _date.AddDateAsync(signedDate);
            int expirationDateId = await _date.AddDateAsync(expirationDate);

            if(signedDateId == 0 || expirationDateId == 0)
            {
                return 0;
            }

            Contract contract = new Contract()
            {
                PdfURL = pdfUrl,
                SignedDateId = signedDateId,
                ExpirationDateId = expirationDateId
            };

            await _db.Contracts.AddAsync(contract);
            await _db.SaveChangesAsync();

            return contract.Id;
        }

        public async Task<bool> DeleteContractAsync(int? id)
        {
            if(id == 0)
            {
                return false;
            }

            Contract contract =  await _db.Contracts
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if(contract != null)
            {
                _db.Contracts.Remove(contract);
                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
