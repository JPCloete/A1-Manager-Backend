using A1_Manager.Interfaces;
using A1_Manager.JoinModels;
using A1_Manager.Models_Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Models
{
    public class Role : IRole
    {
        public Role()
        {
            Employees = new HashSet<EmployeeRole>();
        }
        public int Id { get; set; }

        public int NameId { get; set; }

        [ForeignKey("NameId")]
        public Identity Name { get; set; }

        public string? Description { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<EmployeeRole>? Employees { get; set; } 
    }
}
