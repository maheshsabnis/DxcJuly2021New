using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShredModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace CoteAPI_JWT.Services
{

    /// <summary>
    /// class will be used for following
    /// 1. Register the user
    /// 2. Authenticate user and Generate token
    /// Class Must be injected with
    /// 1. IConfiguration: This will be used to read appsettings.json for SIgneture and Token Expiry Time
    /// 2. USerManager<IdentityUser>: USed for Registering USer
    /// 3. SignInManager<IdentityUSer>: USed fore User SignIn
    /// </summary>
    public class JwtAuthService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public JwtAuthService(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// logic to register new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> RegisterNewUser(RegisterUser user)
        {
            var newUser = new IdentityUser() { UserName = user.Email, Email=user.Email};
            var res = await userManager.CreateAsync(newUser, user.Password);
            if (res.Succeeded)
            {
                return true;  // user created
            }
            return false; // user creation failed
        }

        /// <summary>
        /// Logic for Authenticating user and Generate the Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> AuthUser(LoginUser user)
        {
            string generatedToken = "";

            // Sign in the user
            // the user will be lockedout if 3 attempts failed
            var res = await signInManager.PasswordSignInAsync(user.UserName, user.Password, false, lockoutOnFailure: true);
            if (res.Succeeded)
            {
                // generate toke if the login is successfull
                // get the current login  userb object for Claims aka Payload in Token 
                var loginUser = new IdentityUser(user.UserName);

                // Read Secret Key and Expiration
                var key = Convert.FromBase64String(configuration["JWTSettings:SecretKey"]);
                var expireTime = Convert.ToInt32(configuration["JWTSettings:ExpiryInMinuts"]);

                // Token Description
                var desc = new SecurityTokenDescriptor()
                { 
                    Issuer=null , // This may be the DOmain that isse the Token
                    Audience = null,
                    Subject = new ClaimsIdentity(
                          new List<Claim> { 
                             new Claim("username", loginUser.Id.ToString())
                          }
                        ), // set the Payload, current it wil be the user name
                    Expires = DateTime.UtcNow.AddMinutes(expireTime),
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature), 
                };
                // Generate Token
                var handler = new JwtSecurityTokenHandler();
                var tokenData = handler.CreateJwtSecurityToken(desc);
                // Generate 3 Sections
                generatedToken = handler.WriteToken(tokenData);
            }
            else
            {
                generatedToken = "Login Failed";
            }
            return generatedToken;
        }
    }
}
