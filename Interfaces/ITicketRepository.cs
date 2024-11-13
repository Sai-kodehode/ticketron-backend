using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface ITicketRepository
    {
        Task<ICollection<Ticket>> GetTicketsAsync(Guid bookingId);
        Task<Ticket?> GetTicketAsync(Guid ticketId);
        Task<bool> TicketExistsAsync(Guid ticketId);
        Task<bool> CreateTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(Ticket ticket);
        Task<bool> SaveAsync();
    }
}
