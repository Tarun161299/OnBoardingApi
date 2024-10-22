using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Business.Behaviors
{
    public class JwtAuthenticationDirector
    {
        private readonly string key;

        private readonly Dictionary<string, string> users = new Dictionary<string, string>()
        {{"test","password"},{"test1","pwd"}};

        public JwtAuthenticationDirector(string key)
        {
            this.key= key;
        }

        public string Authenticate(string username,string password)
        {
            if (!users.Any(u => u.Key == username && u.Value == password))
            { return null; }
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
            
    }
}
