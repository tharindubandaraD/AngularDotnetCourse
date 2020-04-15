using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Model;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        /*instance we create two seperate methods to add database photos and users
        we can create one method and specify the type ands save it in db
        */
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();

         Task<IEnumerable<User>> GetUsers();

         Task<User> GetUser(int Id);

         Task<Photo> GetPhoto(int Id);

         
    }
}