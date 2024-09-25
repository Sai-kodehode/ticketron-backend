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

        public bool CreateBooking(Booking booking)
        {
            _context.Add(booking);
            return Save();
        }

        public bool DeleteBooking(Booking booking)
        {
            _context.Remove(booking);
            return Save();
        }

        public Booking GetBooking(int bookingId)
        {
            return _context.Bookings.Where(b => b.Id == bookingId).FirstOrDefault();
        }

        public ICollection<Booking> GetBookings(int userId)
        {
            return _context.Bookings.Where(b => b.User != null && b.User.Id == userId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateBooking(Booking booking)
        {
            _context.Update(booking);
            return Save();
        }
    }
}
