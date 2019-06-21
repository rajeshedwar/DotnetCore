using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityAPI.Infrastructure;
using IdentityAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAPI.Services
{
    public class IdentityManager : IIdentityManager
    {
        private IdentityDbContext db;
        private IConfiguration configuration;
        public IdentityManager(IdentityDbContext dbContext, IConfiguration configuration)
        {
            this.db = dbContext;
            this.configuration = configuration;
        }
        public async Task<dynamic> AddUserAsync(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return new
            {
                user.Id, user.FirstName, user.LastName, UserId = user.Email, user.ContactNo
            };

        }

        public string ValidateUsers(LoginModel user)
        {
            var result = db.Users.SingleOrDefault(c => c.Email == user.email && c.Password == user.password);
            if (result != null)
            {
                string token = GenerateToken(user.email, user.password);
                return token;
            }
            return null;
        }

        private string GenerateToken(string userid, string pwd)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userid),
                new Claim(JwtRegisteredClaimNames.Email,userid),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            if (userid == "Rajesh@hexaware.com")
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            }

            var sercuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")));
            var credentials = new SigningCredentials(sercuritykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
                claims: claimsIdentity.Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
