using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotnetCore_6_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Generate JWT token for Authentication
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("GenerateToken")]
        public IActionResult GenerateToken(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.UserName) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified.");
                if (loginDTO.UserName == "Admin@mandeep.com" 
                    && loginDTO.Password == "Password123")
                {
                    var secureKey = Encoding.UTF8.GetBytes("sgEVycaPeRuMKgwDDevgrUdrGaBVLYaV");

                    var issuer = "Mandeep_Singh";
                    var audience = "Mandeep_Singh";
                    var securityKey = new SymmetricSecurityKey(secureKey);
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

                    var jwtTokenHandler = new JwtSecurityTokenHandler();

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                new Claim("Id", "1"),
                new Claim(JwtRegisteredClaimNames.Sub, loginDTO.UserName),
                new Claim(JwtRegisteredClaimNames.Email, loginDTO.Password),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                        Expires = DateTime.Now.AddMinutes(5),
                        Audience = audience,
                        Issuer = issuer,
                        SigningCredentials = credentials
                    };

                    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = jwtTokenHandler.WriteToken(token);
                    return Ok(jwtToken);
                }
            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();

        }
    }
}
