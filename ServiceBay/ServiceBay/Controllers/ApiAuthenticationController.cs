using Microsoft.AspNetCore.Mvc;
using ServiceBay.Jwt;
using ServiceBay.Middleware;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthenticationController : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator;

        public ApiAuthenticationController(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [Route("UserLogin")]
        [HttpPost]
        public IActionResult UserLogin(Login objVM)
        {
            AuthenticateResponse response = _tokenGenerator.Authenticate(objVM);

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