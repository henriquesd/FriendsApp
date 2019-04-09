using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FriendsApp.API.Data;
using FriendsApp.API.Dtos;
using FriendsApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FriendsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public IConfiguration _config { get; set; }
        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            this._config = config;
            this._repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repository.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repository.Register(userToCreate, userForRegisterDto.Password);

            // TODO: change to return CreatedAtRoute (now don't have a method to currently get the user at this time);
            return StatusCode(201); // 201 is the state code of CreatedAtRoute;
        }

        // example without [ApiController];
        // [HttpPost("register")]
        // public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        // {
        //     // TODO: validate request;

        //     // if don't use the [ApiController], needs to specify a ModelState;
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

        //     if (await _repository.UserExists(userForRegisterDto.Username))
        //         return BadRequest("Username already exists");

        //     var userToCreate = new User
        //     {
        //         Username = userForRegisterDto.Username
        //     };

        //     var createdUser = await _repository.Register(userToCreate, userForRegisterDto.Password);

        //     // TODO: change to return CreatedAtRoute (now don't have a method to currently get the user at this time);
        //     return StatusCode(201); // 201 is the state code of CreatedAtRoute;
        // }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repository.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
               token = tokenHandler.WriteToken(token) 
            });
        }
    }
}