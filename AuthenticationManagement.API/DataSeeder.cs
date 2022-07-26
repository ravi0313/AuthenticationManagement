﻿using Microsoft.AspNetCore.Identity;
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
                FirstName = "Navaneethan",
                LastName = "s",
                Gender = "Male",
                Age = "23",
                DOB = "05/06/1999",
                Email = "navanethan@gmail.com",
                PhoneNumber = "9677656488",
                Address = "Hyderabad",
                UserName = "Admin",
            };
            if (await userManager.FindByNameAsync("Admin") == null)
            {
                await userManager.CreateAsync(Admin, "Admin@1234");
                await userManager.AddToRoleAsync(Admin, "Admin");
                var admindetails = new Admin() { AppUser = Admin, Hospitalname = "CMS", Location = "Hyderabad", Id=Admin.Id };
                context.Admins.Add(admindetails);
                await context.SaveChangesAsync();
            }
        }
        public async Task SeedUsers()
        {
            
            var Doctor = new AppUser
            {
                FirstName = "Nikita",
                LastName = "Kokate",
                Gender = "Female",
                Age = "24",
                DOB = "05/06/1998",
                Email = "nikita@gmail.com",
                PhoneNumber = "9676656488",
                Address = "Hyderabad",
                UserName = "nikita@outlook.com",

            };

            var Patient = new AppUser
            {
                FirstName = "Ravi",
                LastName = "Teja",
                Gender = "Male",
                Age = "23",
                DOB = "05/08/1999",
                Email = "ravi@gmail.com",
                PhoneNumber = "7396542335",
                Address = "Hyderabad",
                UserName = "ravi@outlook.com",

            };

            if (await userManager.FindByNameAsync("nikita@outlook.com") == null)
            {
                await userManager.CreateAsync(Doctor, "Nikita@1234");
                await userManager.AddToRoleAsync(Doctor, "Doctor");
                var doctordetails = new Doctor() { AppUser = Doctor, Education = "MBBS", Experience = "2", SpecializationinDepartment = "Orthology" };
                context.Doctors.Add(doctordetails);
                await context.SaveChangesAsync();
            }

            if (await userManager.FindByNameAsync("ravi@outlook.com") == null)
            {
                await userManager.CreateAsync(Patient, "Ravi@1234");
                await userManager.AddToRoleAsync(Patient, "Patient");
                await context.SaveChangesAsync();
            }
        }
        }
    }
