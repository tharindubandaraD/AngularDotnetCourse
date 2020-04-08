using System;
using System.Collections.Generic;

namespace DatingApp.API.Model
{
    public class User
    {
        //going to store password encripted and add some sault in to it 
        //need to tell this class to entity framwork
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Intersts { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public ICollection<Photo> Photes { get; set; } 
    }
}