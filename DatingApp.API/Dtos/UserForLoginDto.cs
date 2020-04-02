namespace DatingApp.API.Dtos
{
    /*depisode 32 - in asp.net core use to map main models such as user class in to simple objects 
    that automatically get display or retun value*/
    
    public class UserForLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}