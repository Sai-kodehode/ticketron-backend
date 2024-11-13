using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IGroupMemberRepository
    {
        Task<ICollection<GroupMember>> GetGroupMembersAsync(Guid groupId);
        Task<GroupMember?> GetGroupMemberAsync(Guid groupMemberId);
        Task<bool> GroupMemberExistsAsync(Guid groupMemberId);
        Task<bool> CreateGroupMemberAsync(GroupMember groupMember);
        Task<bool> DeleteGroupMemberAsync(GroupMember groupMember);
        Task<bool> SaveAsync();
    }
}