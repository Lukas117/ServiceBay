using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceBay.Contracts;
using ServiceBay.Middleware;
using ServiceBay.Models;
using ServiceBay.Security;

namespace ServiceBay.Jwt
{
    public interface ITokenGenerator
    {
        AuthenticateResponse Authenticate(Login model);
        IEnumerable<Person> GetAll();
        Person GetById(int id);
    }
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IPersonRepository _personRepository;
        private readonly AppSettings _appSettings;
        private readonly Encryption encryption;

        public TokenGenerator(IPersonRepository personRepository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _personRepository = personRepository;
            encryption = new Encryption();
        }

        public AuthenticateResponse Authenticate(Login login)
        {
            Person user = _personRepository.GetPersonByEmail(login.Email).Result;
            // return null if user not found
            if (user == null) return null;

            var passwordHash = user.PasswordHash;
            var salt = user.PasswordSalt;
            var passwordInput = login.Password;

            // authentication successful so generate jwt token
            if (!encryption.AreEqual(passwordInput, passwordHash, salt))
            {
                return null;
            }
            string token = GenerateToken(user);
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetPersons().Result;
        }

        public Person GetById(int id)
        {
            return _personRepository.GetPerson(id).Result;
        }

        public string GenerateToken(Person person)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", person.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
