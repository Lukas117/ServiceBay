using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceBay.Contracts;
using ServiceBay.Jwt;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthenticationController : ControllerBase
    {
        private readonly IPersonRepository _personRepo;
        

        public ApiAuthenticationController(IPersonRepository personRepository)
        {
            _personRepo = personRepository;
        }

        [Route("UserLogin")]
        [HttpPost]
        public IActionResult UserLogin([FromBody] Login objVM)
        {
            if(_personRepo.GetPersonByEmail(objVM.Email).Result.PasswordHash.Equals(objVM.Password))
            {
                var tokenString = TokenGenerator.GenerateToken(objVM.Email);
                return Ok(new { Token = tokenString, Message = "Success" });
            }
            return BadRequest();



            //            var objlst = wmsEN.Usp_Login(objVM.Email, Utility.Encryptdata(objVM.Password), "").ToList<Usp_Login_Result>().FirstOrDefault();
            //          if (objlst.Status == -1) return new Response
            //        {
            //          Status = "Invalid",
            //        Message = "Invalid User."
            //  };
            //if (objlst.Status == 0) return new Response
            //{
            //    Status = "Inactive",
            //    Message = "User Inactive."
            //  };
            //            else return new Response
            //           {
            //               Status = "Success",
            //               Message = TokenGenerator.GenerateToken(objVM.Email)
            //           };
            
            
                
            
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("API Validated");
        }

        [Route("Validate")]
        [HttpGet]
        public Response Validate(string token, string username)
        {
            int UserId = _personRepo.GetPersonByEmail(username).Id;
            if (UserId == 0) return new Response
            {
                Status = "Invalid",
                Message = "Invalid User."
            };
            string tokenUsername = TokenGenerator.ValidateToken(token);
            if (username.Equals(tokenUsername))
            {
                return new Response
                {
                    Status = "Success",
                    Message = "OK",
                };
            }
            return new Response
            {
                Status = "Invalid",
                Message = "Invalid Token."
            };
        }

        

    }


}