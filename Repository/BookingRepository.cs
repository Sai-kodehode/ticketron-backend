using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;
        public BookingRepository(DataContext context)
        {
            _context = context;
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
            return await _context.Bookings.Where(b => b.Id == bookingId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Booking>> GetBookingsAsync(Guid userId)
        {
            return await _context.Bookings.Where(b => b.User != null && b.User.Id == userId).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0;
        }
    }
}
