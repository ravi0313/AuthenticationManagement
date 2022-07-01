using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AuthenticationManagementContext context;
        private readonly IConfiguration configuration;
        public SigninController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AuthenticationManagementContext context, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.configuration = configuration;
        }



        [HttpPost]
        public async Task<IActionResult> Signin(LoginDTO dto)
        {
            AppUser user = await userManager.FindByNameAsync(dto.Username);
            if (user == null)
                return BadRequest("Invalid username/password");
            bool IsValidPassword = await userManager.CheckPasswordAsync(user, dto.Password);
            if (!IsValidPassword)
                return BadRequest("Invalid username/password");

            string key = configuration["JWT:key"];
            string issuer = configuration["JWT:issuer"];
            string audience = configuration["JWT:audience"];

            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            DateTime expires = DateTime.Now.AddMinutes(30);
            SecurityKey securityKey = new SymmetricSecurityKey(keyBytes);

            var userClaims = await userManager.GetClaimsAsync(user);
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var userRoles = await userManager.GetRolesAsync(user);
            var role = userRoles.First();
            userClaims.Add(new Claim(ClaimTypes.Role, role));


            SigningCredentials credentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: userClaims,
                signingCredentials: credentails,
                expires: expires);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string jwt = handler.WriteToken(token);

            string userdetails = string.Empty;
            if (role == "Patient")
            {
                var patientdetails = context.Patients.First(i => i.AppUser.Id == user.Id);
                userdetails = patientdetails.Diagnosis;
            }
            else if (role == "Doctor")
            {
                var doctordetails = context.Doctors.First(i => i.AppUser.Id == user.Id);
                userdetails = doctordetails.Department;
            }
            string admindetails = string.Empty;
            var response = new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Age = user.Age,
                Occupation = user.Occupation,
                Email_id = user.Email,
                Phonenumber = user.PhoneNumber,
                role = role,
                token = jwt
            };
            return Ok(response);
        }

    }
}