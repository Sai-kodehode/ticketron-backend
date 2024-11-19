using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class UnregUserRepository : IUnregUserRepository
    {
        private readonly DataContext _context;

        public UnregUserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUnregUserAsync(UnregUser unregUser)
        {
            await _context.AddAsync(unregUser);
            return await SaveAsync();
        }

        public async Task<bool> DeleteUnregUserAsync(UnregUser unregUser)
        {
            _context.Remove(unregUser);
            return await SaveAsync();
        }

        public async Task<UnregUser?> GetUnregUserAsync(Guid unregUserId)
        {
            return await _context.UnregUsers
                .Include(u => u.CreatedBy)
                .FirstOrDefaultAsync(x => x.Id == unregUserId);
        }

        public async Task<ICollection<UnregUser>> GetUnregUsersByIdsAsync(ICollection<Guid> unregUserIds)
        {
            return await _context.UnregUsers
                .Where(x => unregUserIds.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<ICollection<UnregUser>> GetUnregUsersByUserIdAsync(Guid userId)
        {
            return await _context.UnregUsers
                .Include(u => u.CreatedBy)
                .Where(x => x.CreatedBy.Id == userId)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UnregUserExistsAsync(Guid unregUserId)
        {
            return await _context.UnregUsers
                .AnyAsync(x => x.Id == unregUserId);
        }
    }
}