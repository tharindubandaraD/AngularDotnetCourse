using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
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

         Task<PageList<User>> GetUsers(UserParams userParams);

         Task<User> GetUser(int Id);

         Task<Photo> GetPhoto(int Id);

         Task<Photo> GetMainPhotoForUser(int userId);

         Task<Like> GetLike(int userId, int recipientId);

         Task<Message> GetMessage(int id);

         Task<PageList<Message>> GetMessagesForUser(MessageParams messageParams);
         
         Task<IEnumerable<Message>> GetMessagesTread(int userId, int recipientId);
         
    }
}