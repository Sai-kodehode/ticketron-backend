using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetBookingAsync(Guid bookingId);
        Task<ICollection<Booking>> GetBookingsAsync(Guid userId);
        Task<bool> CreateBookingAsync(Booking booking);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(Booking booking);
        Task<bool> SaveAsync();
        Task<bool> BookingExistsAsync(Guid bookingId);
    }
}
