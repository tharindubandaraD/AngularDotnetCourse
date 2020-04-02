using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{

    //31 episode    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        //inject newly created repo : episode 31
        private IAuthRepository _repo { get; }
        //
        private readonly IConfiguration _confg;
        public AuthController(IAuthRepository repo, IConfiguration confg)
        {
            this._confg = confg;
            _repo = repo;
        }

        [HttpPost("register")]
        //we are using DTO to parse into register method 
        public async Task<IActionResult> Register(UserForRegisterDtos userForRegisterDtos)
        {
            //episode 31 - 
            //validate request 
            userForRegisterDtos.Username = userForRegisterDtos.Username.ToLower();

            if (await _repo.UserExists(userForRegisterDtos.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = userForRegisterDtos.Username
            };

            var createUser = await _repo.Register(userToCreate, userForRegisterDtos.Password);

            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFormRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFormRepo == null)
                return Unauthorized();

            //to store token
            var claims = new[]  
            {
               new Claim(ClaimTypes.NameIdentifier , userFormRepo.Id.ToString()),
               new Claim(ClaimTypes.Name , userFormRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confg.GetSection("AppSettings:Token").Value));

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
