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
                .Include(g => g.CreatedBy)
                .Include(g => g.Users)
                .Include(g => g.UnregUsers)
                .Where(x => x.Id == groupId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Group>> GetGroupsByIdsAsync(ICollection<Guid> groupIds)
        {
            return await _context.Groups
                .Include(g => g.CreatedBy)
                .Include(g => g.Users)
                .Include(g => g.UnregUsers)
                .Where(x => groupIds.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<ICollection<Group>> GetGroupsByUserIdAsync(Guid userId)
        {
            return await _context.Groups
                .Include(g => g.CreatedBy)
                .Include(g => g.Users)
                .Include(g => g.UnregUsers)
                .Where(x => x.CreatedBy.Id == userId)
                .ToListAsync();
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
    }
}
