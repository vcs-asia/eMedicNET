using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using eMedicEntityModel.Models.v1;

namespace eMedicNETv6.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost(nameof(Login))]
        [Consumes("application/x-www-form-urlencoded")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] Login model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.Email);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var issuer = _configuration.GetSection("Jwt:Issuer").Value;
                var audience = _configuration.GetSection("Jwt:Audience").Value;
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Key").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
                         }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(new ResponseModel { Message = "", Flag = true, Data = user, Token = stringToken });
            }

            return Unauthorized();

            /*
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.Email);
            }
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return Ok(new ResponseModel { Message = "", Token = GenerateJwtToken(user), Data = user, Flag = true });
                }
                else if (result.IsLockedOut)
                {
                    return Ok(new ResponseModel { Message = "You have been locked out. Please contact the administrator.", Data = null, Flag = false });
                }
                else
                {
                    return Ok(new ResponseModel { Message = "Invalid Credentials. Please try again.", Data = null, Flag = false });
                }
            }

            return BadRequest("Invalid Request");*/
        }

        /*
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetResult/{id}")]
        public IActionResult GetResult(string id)
        {
            return Ok("API Validated");
        }*/

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.Id),
                new Claim("DisplayName", user.UserName),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
