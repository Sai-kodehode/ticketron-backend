using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await _context.AddAsync(user);
            return await SaveAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _context.Users.OrderBy(u => u.Id).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await _context.Users.AnyAsync(x => x.Id == userId);
        }
    }
}
