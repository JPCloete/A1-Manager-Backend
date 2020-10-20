using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Main_Interfaces
{
    public interface IBrand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoURL { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }
    }
}
