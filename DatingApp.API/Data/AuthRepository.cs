using System;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    //this is concreate class    
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        //ctor
        public AuthRepository(DataContext context)
        {
            _context = context;
        }       

        //29 th caption
        public async Task<User> Login(string username, string password)
        {
            //take data from database ad save on var user
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
                return null;
            //this will return true or false
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;

        }

        //29 th caption
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //same as create password hash pass passwordsalt
             using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));  
               for(int i=0; i< computedHash.Length;i++)
               {
                   //compair both computed and password hashes 
                   if(computedHash[i] != passwordHash[i]) return false;
               }              
            }
            return true;
        }
        //28th caption
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //add changes to database
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            //returning the user
            return user;
        }
        //28th caption 
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));                
            }
        }

        //29 episode - logic for user exisit 
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}