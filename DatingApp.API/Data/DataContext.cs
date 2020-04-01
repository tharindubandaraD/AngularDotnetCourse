using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    //dirive entity framework class - intall microsoft.entityframeworkcore
    public class DataContext : DbContext
    {
        /*
        DbContextOptions - The database provider to use, typically selected by invoking a 
            method such as UseSqlServer or UseSqlite. These extension methods require
         the corresponding provider package, such as 
         Microsoft.EntityFrameworkCore.SqlServer or Microsoft.EntityFrameworkCore.Sqlite. 
         The methods are defined in the Microsoft.EntityFrameworkCore namespace.
         * Any necessary connection string or identifier of the database instance, typically passed as 
         an argument to the provider selection method mentioned above
         * Any provider-level optional behavior selectors, typically also chained inside the call to 
         the provider selection method
         * Any general EF Core behavior selectors, typically chained after or before the provider 
         selector method

         */
         public DataContext(DbContextOptions<DataContext> options) : base (options) { }
        
        //table name is here 
         public DbSet<Value>  Values { get; set; }

        /*after we added new table like this need to run migration script next
        -dotnet ef migrations add AddedUserEntity
        -dotnet ef database update
        */
         public DbSet<User> Users { get;set; }

    }
}