using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public GroupRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
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
            var currentUserId = _userContextService.GetUserObjectId();

            return await _context.Groups
                .Include(g => g.CreatedBy)
                .Include(g => g.Users)
                .Include(g => g.UnregUsers)
                .FirstOrDefaultAsync(g=>g.Id==groupId && g.CreatedById==currentUserId);
        }

        public async Task<ICollection<Group>> GetGroupsByIdsAsync(ICollection<Guid> groupIds)
        {

            var currentUserId = _userContextService.GetUserObjectId();

            return await _context.Groups
                .Include(g => g.CreatedBy)
                .Include(g => g.Users)
                .Include(g => g.UnregUsers)
                .Where(x => groupIds.Contains(x.Id) && x.CreatedById==currentUserId)
                .ToListAsync();
        }

        public async Task<ICollection<Group>> GetGroupsAsync()
        {
            var currentUserId = _userContextService.GetUserObjectId();

            return await _context.Groups
                .Include(g => g.CreatedBy)
                .Include(g => g.Users)
                .Include(g => g.UnregUsers)
                .Where(x => x.CreatedBy.Id == currentUserId)
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
