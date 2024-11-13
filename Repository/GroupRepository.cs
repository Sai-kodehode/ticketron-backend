using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateGroupAsync(Group group)
        {
            await _context.AddAsync(group);
            return await SaveAsync();
        }

        public async Task<bool> DeleteGroupAsync(Group group)
        {
            _context.Remove(group);
            return await SaveAsync();
        }

        public async Task<Group?> GetGroupAsync(Guid groupId)
        {
            return await _context.Groups
                .Include(x => x.User)
                .Include(x => x.GroupMembers)
                .Where(x => x.Id == groupId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Group>> GetGroupsAsync(Guid userId)
        {
            return await _context.Groups.Where(x => x.User.Id == userId).ToListAsync();
        }

        public async Task<bool> GroupExistsAsync(Guid groupId)
        {
            return await _context.Groups.AnyAsync(x => x.Id == groupId);
        }

        public async Task<bool> SaveAsync()
        {
            var Saved = await _context.SaveChangesAsync();
            return Saved > 0;
        }

        public async Task<bool> UpdateGroupAsync(Group group)
        {
            var existingGroup = await _context.Groups.FindAsync(group.Id);
            if (existingGroup == null)
                return false;

            _context.Entry(existingGroup).State = EntityState.Detached;

            _context.Groups.Attach(group);
            _context.Entry(group).State = EntityState.Modified;

            return await SaveAsync();

        }
    }
}
