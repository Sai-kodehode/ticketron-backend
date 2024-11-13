using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTicketAsync(Ticket ticket)
        {
            await _context.AddAsync(ticket);

            return await SaveAsync();
        }

        public async Task<bool> DeleteTicketAsync(Ticket ticket)
        {
            _context.Remove(ticket);

            return await SaveAsync();
        }

        public async Task<Ticket?> GetTicketAsync(Guid ticketId)
        {
            return await _context.Tickets.Where(x => x.Id == ticketId).FirstOrDefaultAsync();
        }


        public async Task<ICollection<Ticket>> GetTicketsAsync(Guid bookingId)
        {
            return await _context.Tickets.Where(x => x.Booking.Id == bookingId).ToListAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
        public async Task<bool> TicketExistsAsync(Guid ticketId)
        {
            return await _context.Tickets.AnyAsync(x => x.Id == ticketId);
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            var existingTicket = await _context.Tickets.FindAsync(ticket.Id);
            if (existingTicket == null)
                return false;

            _context.Entry(existingTicket).State = EntityState.Detached;
            _context.Update(ticket);
            return await SaveAsync();

        }
    }
}
