using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceBay.Contracts;

namespace ServiceBay.Jwt
{
    public class TokenGenerator
    {
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        public static IPersonRepository personRepository;
        public static IConfiguration _configuration;

        public TokenGenerator(IPersonRepository person, IConfiguration configuration)
        {
            personRepository = person;
            _configuration = configuration;
        }

        public static string GenerateToken(string username)
        {
            //  if(!personRepository.GetPersonByEmail(username).Result.PasswordHash.Equals(password))
            //  {
            //      return null;
            //  }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "https://localhost:5001",
                Audience = "https://localhost:5001",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
