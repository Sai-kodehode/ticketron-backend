using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IUnregUserRepository
    {
        UnregUser GetUnregUser(int unregUserId);
        ICollection<UnregUser> GetUnregUsersByUserId(int userId);
        bool UnregUserExists(int unregUserId);
        bool CreateUnregUser(UnregUser unregUser);
        bool DeleteUnregUser(UnregUser unregUser);
        bool Save();
    }
}
