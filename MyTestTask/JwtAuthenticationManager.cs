using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MyTestTask.Data;

namespace MyTestTask
{
    public class JwtAuthenticationManager
    {
        private readonly string? key;

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }
        public string? Authenticate(string? username,bool Admin, ApplicationDbContext _db)
        {
            if (_db.Persons != null && !_db.Persons.Any(u=>u.Name == username && u.Admin == Admin))
            return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(key);
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
