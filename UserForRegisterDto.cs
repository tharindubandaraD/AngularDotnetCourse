using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    //creted new dto to register user
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Not enough length")]
        public string Password { get; set; }
    }
}