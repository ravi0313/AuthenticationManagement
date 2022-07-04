using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationManagement.API
{
    public class AuthenticationManagementContext : IdentityDbContext<AppUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public AuthenticationManagementContext(DbContextOptions options) : base(options)
        {
        }
    }
}