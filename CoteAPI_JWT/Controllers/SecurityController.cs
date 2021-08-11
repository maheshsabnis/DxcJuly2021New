using CoteAPI_JWT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShredModels;
namespace CoteAPI_JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly JwtAuthService serv;
        public SecurityController(JwtAuthService serv)
        {
            this.serv = serv;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await serv.RegisterNewUser(user);
                if (!result)
                {
                    // return error for USer Already Exists
                    return Conflict($"{user.Email} is already present");
                }
                var response = new ResponseData()
                {
                     Message = $"The New User is Registered with User Name as {user.Email}"
                };
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AuthAsync(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await serv.AuthUser(user);
                if (String.IsNullOrEmpty(result))
                {
                   
                    return Unauthorized($"{user.UserName} Login Failed");
                }
                var response = new ResponseData()
                {
                    Message = result //this will return token
                };
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
