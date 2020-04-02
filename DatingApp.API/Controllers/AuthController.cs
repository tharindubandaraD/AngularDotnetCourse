using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
<<<<<<< HEAD

=======
   
>>>>>>> 02-04-2020 Code Commit
namespace DatingApp.API.Controllers
{
    //apicontroller will says from where data is comming from so its normally allow null to empty strings.
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //inject repo        
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        /*IActionResult is an interface, we can create a 
       custom response as a return, when you use ActionResult 
       you can return only predefined ones for returning a View or a resource
       --ViewResult, PartialViewResult, JsonResult, etc., derive from ActionResult.
       */
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //this not working because this will return User object as json serializable object so need to place a DTO
            // validate the request - all we need to convert lowercase

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("User Exsist");

            //this will pass username
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };
            //this send the second password
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            //tempory we return status code
            return StatusCode(201);


        }

        [HttpPost("login")] 
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null)
                return Unauthorized();
<<<<<<< HEAD

=======
            //buit a token to return to user it has id and Uname - store them in claims
>>>>>>> 02-04-2020 Code Commit
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };
<<<<<<< HEAD

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

=======
            //store key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            
>>>>>>> 02-04-2020 Code Commit
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
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