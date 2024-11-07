using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(Guid azureObjectId);
        bool UserExists(Guid azureObjectId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
