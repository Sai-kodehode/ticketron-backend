using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IBookingRepository
    {
        Booking GetBooking(int bookingId);
        ICollection<Booking> GetBookings(int userId);
        bool CreateBooking(Booking booking);
        bool UpdateBooking(Booking booking);
        bool DeleteBooking(Booking booking);
        bool Save();
        bool BookingExists(int bookingId);
    }
}
