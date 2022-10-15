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
        public string? Authenticate(string? username,string? password,string? number, ApplicationDbContext _db)
        {
            if (_db.Persons != null && !_db.Persons.Any(u=>u.Name == username && u.Password == password && u.Number == number))
            return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            Debug.Assert(key != null, nameof(key) + " != null");
            var tokenkey = Encoding.ASCII.GetBytes(key);
            Debug.Assert(number != null, nameof(number) + " != null");
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,number)
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
