using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationManagement.API
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Education { get; set; }
        public string Specialization { get; set; }
        public string Department { get; set; }
        public string Experience { get; set; }
        public AppUser AppUser { get; set; }
    }
}