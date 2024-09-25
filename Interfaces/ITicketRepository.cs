using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface ITicketRepository
    {
        ICollection<Ticket> GetTickets(int bookingId);
        Ticket GetTicket(int ticketId);
        bool TicketExists(int ticketId);
        bool CreateTicket(Ticket ticket);
        bool UpdateTicket(Ticket ticket);
        bool DeleteTicket(Ticket ticket);
        bool Save();
    }
}
