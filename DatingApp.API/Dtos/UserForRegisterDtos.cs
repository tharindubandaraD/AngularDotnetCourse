using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDtos
    {
        //data transfer object
        [Required]
        public string Username { get; set; }
        
        [Required]
        [StringLength(10,MinimumLength = 4 , ErrorMessage = "You must specify password length 4 to 10" )]
        public string  Password { get; set; }
    }
}