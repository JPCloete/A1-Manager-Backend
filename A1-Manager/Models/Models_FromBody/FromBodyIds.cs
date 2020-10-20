using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_FromBody
{
    //Flexible class used to make FromBody requests when multiple id's are required
    public class FromBodyIds
    {
        public virtual int AmountId { get; set; }

        public virtual int BranchId { get; set; }

        public virtual int BranchSaleId { get; set; }

        public virtual int BrandId { get; set; }

        public virtual int BrandSaleId { get; set; }

        public virtual int CityId { get; set; }

        public virtual int ContractId { get; set; }

        public virtual int CountryId { get; set; }

        public virtual int DateId { get; set; }

        public virtual int EmployeeId { get; set; }

        public virtual int IdentityId { get; set; }

        public virtual int MoneyId { get; set; }

        public virtual int OrderId { get; set; }

        public virtual int ProductId { get; set; }

        public virtual int ProductSaleId { get; set; }

        public virtual int RoleId { get; set; }

        public virtual int SupplierId { get; set; }
    }
}
