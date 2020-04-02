using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
         public  DataContext(DbContextOptions<DataContext> options) : base(options){
        }
        public DbSet<Values> Values {get;set;}

        //25 th clip after that run update migration 
        public DbSet<User> Users { get; set; }

    }
}