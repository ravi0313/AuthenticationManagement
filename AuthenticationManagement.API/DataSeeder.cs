using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationManagement.API
{
    public class DataSeeder
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly AuthenticationManagementContext context;

        public DataSeeder(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AuthenticationManagementContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task SeedRoles()
        {
            var role1 = new IdentityRole { Name = "Patient" };
            var role2 = new IdentityRole { Name = "Doctor" };
            var role3 = new IdentityRole { Name = "Admin" };
            if (!await roleManager.RoleExistsAsync("Patient"))
                await roleManager.CreateAsync(role1);
            if (!await roleManager.RoleExistsAsync("Doctor"))
                await roleManager.CreateAsync(role2);
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(role3);
        }

        public async Task SeedAdmin()
        {
            var Admin = new AppUser
            {
                Id = "101",
                UserName = "Admin",
            };
            if (await userManager.FindByNameAsync("Admin") == null)
            {
                await userManager.CreateAsync(Admin, "Admin@1234");
                await userManager.AddToRoleAsync(Admin, "Admin");
                var doctordetails = new Doctor() { AppUser = Admin };
            }
        }
        public async Task SeedUsers()
        {
            var Doctor = new AppUser
            {
                FirstName = "Gal",
                LastName = "Gadot",
                Gender = "Female",
                Occupation = "Doctor",
                Age = "32",
                DOB = "05/06/1990",
                Address = "Hyderabad",
                UserName = "gal@outlook.com",

            };

            var Patient = new AppUser
            {
                FirstName = "Ravi",
                LastName = "Teja",
                Gender = "Male",
                Occupation = "Student",
                Age = "23",
                DOB = "05/08/1999",
                Address = "Hyderabad",
                UserName = "ravi@outlook.com",

            };

            if (await userManager.FindByNameAsync("gal@outlook.com") == null)
            {
                await userManager.CreateAsync(Doctor, "Gal@1234");
                await userManager.AddToRoleAsync(Doctor, "Doctor");
                var doctordetails = new Doctor() { AppUser = Doctor };
                context.Doctors.Add(doctordetails);
                await context.SaveChangesAsync();
            }

            if (await userManager.FindByNameAsync("ravi@outlook.com") == null)
            {
                await userManager.CreateAsync(Patient, "Ravi@1234");
                await userManager.AddToRoleAsync(Patient, "Patient");
                var patientdeetails = new Patient() { AppUser = Patient };
                context.Patients.Add(patientdeetails);
                await context.SaveChangesAsync();
            }
        }
    }
}
