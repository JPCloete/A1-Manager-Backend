using A1_Manager.Main_Interfaces;
using A1_Manager.Models_Joins;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models_Main
{
    public class Employee : IEmployee
    {

        public Employee()
        {
            Roles = new HashSet<EmployeeRole>();
        }

        public int Id { get; set; }

        public int FirstNameId { get; set; }

        [ForeignKey("FirstNameId")]
        public Identity FirstName { get; set; }

        public int LastNameId { get; set; }

        [ForeignKey("LastNameId")]
        public Identity LastName { get; set; }

        public string ImageURL { get; set; }

        public int Presence { get; set; }

        public int LeaveRemaining { get; set; }

        public int SalaryId { get; set; }

        [ForeignKey("SalaryId")]
        public virtual Money Salary { get; set; }

        public int ContractId { get; set; }

        public virtual Contract Contract { get; set; }

        public int BranchId { get; set; }

        public Branch Branch { get; set; }

        public virtual ICollection<EmployeeRole>? Roles { get; set; }

    }
}
