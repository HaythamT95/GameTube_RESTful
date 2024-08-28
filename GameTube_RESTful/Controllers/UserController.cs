using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTube_RESTful.Services;
using GameTube_RESTful.Models;

namespace GameTube_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserServices userServices) : ControllerBase
    {
        private readonly UserServices _userServices = userServices;

        [HttpGet("getallusers")]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = _userServices.GetAll();
            return Ok(users);
        }

        [HttpGet("getuserbyid/{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userServices.GetUserById(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPost("adduser")]
        public ActionResult AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userServices.AddUser(user);

            return Ok("User added successfully.");
        }
    }
}
