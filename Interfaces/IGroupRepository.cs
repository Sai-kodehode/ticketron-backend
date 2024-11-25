using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IGroupRepository
    {
        Task<ICollection<Group>> GetGroupsAsync();
        Task<Group?> GetGroupAsync(Guid groupId);
        Task<ICollection<Group>> GetGroupsByIdsAsync(ICollection<Guid> groupIds);
        Task<bool> GroupExistsAsync(Guid groupId);
        Task<bool> CreateGroupAsync(Group group);
        Task<bool> DeleteGroupAsync(Group group);
        Task<bool> SaveAsync();
    }
}