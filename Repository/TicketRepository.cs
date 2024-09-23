using Ticketron.Data;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class TicketRepository:ITicketRepository
    {
        private readonly DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Ticket> GetTickets()
        {
            return _context.Tickets.OrderBy(t => t.Id).ToList();

        }
    }
}
