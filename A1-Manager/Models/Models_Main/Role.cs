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
    public class Role : IRole
    {
        public Role()
        {
            Employees = new HashSet<EmployeeRole>();
        }
        public virtual int Id { get; set; }

        public virtual int NameId { get; set; }

        [ForeignKey("NameId")]
        public virtual Identity Name { get; set; }

        public virtual string? Description { get; set; }

        public virtual int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<EmployeeRole>? Employees { get; set; } 
    }
}
