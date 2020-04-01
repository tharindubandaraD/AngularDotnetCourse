namespace DatingApp.API.Models
{
    public class User
    {
        //going to store password encripted and add some sault in to it 
        //need to tell this class to entity framwork
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}