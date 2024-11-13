using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IUnregUserRepository
    {
        Task<UnregUser?> GetUnregUserAsync(Guid unregUserId);
        Task<ICollection<UnregUser>> GetUnregUsersByUserIdAsync(Guid userId);
        Task<bool> UnregUserExistsAsync(Guid unregUserId);
        Task<bool> CreateUnregUserAsync(UnregUser unregUser);
        Task<bool> DeleteUnregUserAsync(UnregUser unregUser);
        Task<bool> SaveAsync();
    }
}
