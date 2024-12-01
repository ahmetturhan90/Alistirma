using Alistirma.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alistirma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Jwt : ControllerBase
    {
        private readonly IConfiguration _config;
        public Jwt(IConfiguration config)
        {
            _config = config;
        }
        //For admin Only
        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return Ok("You Are Admin");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] string userName,string password)
        {
            var token = GenerateToken(userName);
            if (token != null)
            {
                return Ok(token);
            }

            return NotFound("user not found");
        }
        private string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,username),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
       
    }
}
