using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceBay.Contracts;
using ServiceBay.Jwt;
using ServiceBay.Middleware;
using ServiceBay.Models;
using ServiceBay.Security;

namespace ServiceBay.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthenticationController : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly Encryption encryption;

        public ApiAuthenticationController(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            encryption = new Encryption();
        }

        [Route("UserLogin")]
        [HttpPost]
        public IActionResult UserLogin(Login objVM)
        {
            var response = _tokenGenerator.Authenticate(objVM);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response.Token);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Api validated");
        }
    }
}