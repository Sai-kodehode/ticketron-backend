using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public BookingRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<bool> BookingExistsAsync(Guid bookingId)
        {
            return await _context.Bookings.AnyAsync(b => b.Id == bookingId);
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            _context.Add(booking);

            return await SaveAsync();
        }

        public async Task<bool> DeleteBookingAsync(Booking booking)
        {
            _context.Remove(booking);

            return await SaveAsync();
        }

        public async Task<Booking?> GetBookingAsync(Guid bookingId)
        {
            var currentUserId = _userContextService.GetUserObjectId();

            return await _context.Bookings
                .Include(b => b.CreatedBy)
                .Include(b => b.Users)
                .Include(b => b.UnregUsers)
                .Include(b => b.Tickets)
                .Include(b => b.Groups)
                    .ThenInclude(g => g.Users)
                .FirstOrDefaultAsync(b =>
                b.Id == bookingId &&
                (b.CreatedById == currentUserId ||
                 b.Users.Any(u => u.Id == currentUserId) ||
                 b.Groups.Any(g => g.Users.Any(u => u.Id == currentUserId))));
        }

        public async Task<ICollection<Booking>> GetBookingsAsync()
        {
            var currentUserId = _userContextService.GetUserObjectId();

            return await _context.Bookings
                .Include(b => b.CreatedBy)
                .Include(b => b.Users)
                .Include(b => b.UnregUsers)
                .Include(b => b.Tickets)
                .Include(b => b.Groups)
                    .ThenInclude(g => g.Users)
                .Where(b =>
                    b.CreatedById == currentUserId ||
                    b.Users.Any(u => u.Id == currentUserId) ||
                    b.Groups.Any(g => g.Users.Any(u => u.Id == currentUserId)))
                .OrderBy(b => b.StartDate)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0;
        }
    }
}
