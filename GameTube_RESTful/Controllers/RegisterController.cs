using GameTube_RESTful.Models;
using GameTube_RESTful.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameTube_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController(UserServices userServices) : ControllerBase
    {
        private readonly UserServices _userServices = userServices;

        [HttpPost("register")]
        public ActionResult Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the user already exists by email
            if (_userServices.UserExists(user.Email))
            {
                return BadRequest("User with this email already exists.");
            }

            // Hash the password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            // Add the user to the database
            _userServices.AddUser(user);

            return Ok("User registered successfully.");
        }
    }
}
