using GameTube_RESTful.Models;
using GameTube_RESTful.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameTube_RESTful.Settings;
using Microsoft.Extensions.Options;


namespace GameTube_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(UserServices userServices, IOptions<JwtSettings> jwtSettings) : ControllerBase
    {
        private readonly UserServices _userServices = userServices;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        [HttpPost("login")]
        public ActionResult Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userServices.GetUserByEmail(login.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _jwtSettings.GenerateJwtToken(user);

            return Ok(new { token });
        }
    }
}
