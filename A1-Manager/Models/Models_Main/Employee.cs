using A1_Manager.Main_Interfaces;
using A1_Manager.Models.Models_Main;
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
            Status = "Not Working";
        }

        public virtual int Id { get; set; }

        public virtual int FirstNameId { get; set; }

        [ForeignKey("FirstNameId")]
        public virtual Identity FirstName { get; set; }

        public virtual int LastNameId { get; set; }

        [ForeignKey("LastNameId")]
        public virtual Identity LastName { get; set; }

        public virtual string ImageURL { get; set; }

        public virtual string? Status { get; set; } 

        public virtual int LeaveRemaining { get; set; }

        public virtual int SalaryId { get; set; }

        [ForeignKey("SalaryId")]
        public virtual Money Salary { get; set; }

        public virtual int? ContractId { get; set; }
        [ForeignKey("ContractId")]
        public virtual Contract? Contract { get; set; }

        public virtual int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ICollection<EmployeeRole>? Roles { get; set; }

        public virtual ICollection<EmployeePresence>? Presence { get; set; }

    }
}
