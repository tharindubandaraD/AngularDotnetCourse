using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    ////using  repository pattern to store data - concreate class
    //concreate class that implement from Iauthrepository 
    public class AuthRepository : IAuthRepository
    {
         private readonly DataContext _context ;
        
        //inject our data context in constructor
        public AuthRepository(DataContext context)
        {
           _context = context;
        }

       /*after register need to login - password is hash it need to compute and compaire 
       pw in db
       */
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
                return null;
            
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        /*
        same as create password but this time we pass the key  in for loop 

        */
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {     
                //this will computed hash from password  - computed hash is a byte array so it need to iterate
                        
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for(int i=0; i< computedHash.Length; i++)
               {
                   if(computedHash[i] != passwordHash[i]) return false ; 
               }
            }
            return true;
        }
        //this is the first method we going to modify
        public async Task<User> Register(User user, string password)
        {
           byte[] passwordHash,passwordSalt;
           CreatePasswordHash(password,out passwordHash,out passwordSalt);

           user.PasswordHash = passwordHash;
           user.PasswordSalt = passwordSalt;

           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();

           return user;
        }

        /*this is the passwordhash method we are hashing pw - 
        use system.security.cryptography
        HMACSHA512- will generate randomly generate hashkey

        go to definition HMACsha512 -> HMAC - > Keyedhashalgorithm -> hashalgorithm -> dispose 
        dipose will relase all in current instance
        */

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //this because of calling dispose
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        //after create this we need to register this repo as a service - startup class
        public  async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}