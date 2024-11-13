using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class GroupMemberRepository : IGroupMemberRepository
    {
        private readonly DataContext _context;

        public GroupMemberRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateGroupMemberAsync(GroupMember groupMember)
        {
            await _context.GroupMembers.AddAsync(groupMember);
            return await SaveAsync();
        }

        public async Task<bool> DeleteGroupMemberAsync(GroupMember groupMember)
        {
            _context.GroupMembers.Remove(groupMember);
            return await SaveAsync();
        }

        public async Task<GroupMember?> GetGroupMemberAsync(Guid groupMemberId)
        {
            var groupMember = await _context.GroupMembers
                .Include(gm => gm.User)
                .Include(gm => gm.UnregUser)
                .FirstOrDefaultAsync(gm => gm.Id == groupMemberId);

            return groupMember;
        }

        public async Task<ICollection<GroupMember>> GetGroupMembersAsync(Guid groupId)
        {
            return await _context.GroupMembers
                .Include(gm => gm.User)
                .Include(gm => gm.UnregUser)
                .Where(x => x.Group.Id == groupId)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> GroupMemberExistsAsync(Guid groupMemberId)
        {
            return await _context.GroupMembers.AnyAsync(x => x.Id == groupMemberId);
        }
    }
}