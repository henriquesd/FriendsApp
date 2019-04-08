using System.Threading.Tasks;
using FriendsApp.API.Data;
using FriendsApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FriendsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public AuthController(IAuthRepository repository)
        {
            this._repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            // TODO: validate request;

            username = username.ToLower();

            if (await _repository.UserExists(username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = username
            };

            var createdUser = await _repository.Register(userToCreate, password);

            // TODO: change to return CreatedAtRoute (now don't have a method to currently get the user at this time);
            return StatusCode(201); // 201 is the state code of CreatedAtRoute;
        }
    }
}