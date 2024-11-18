using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsersAllAsync();
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<ICollection<User>> GetUsersByIdsAsync(ICollection<Guid> userIds);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(Guid userId);
        Task<bool> CreateUserAsync(User user);
        Task<bool> SaveAsync();
    }
}