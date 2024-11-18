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
            return await _context.Tickets
                .Include(t => t.PurchasedBy)
                .Include(t => t.AssignedUser)
                .Include(t => t.AssignedUnregUser)
                .FirstOrDefaultAsync(t => t.Id == ticketId); ;
        }

        public async Task<ICollection<Ticket>> GetTicketsAsync(Guid bookingId)
        {
            return await _context.Tickets
                .Include(t => t.PurchasedBy)
                .Include(t => t.AssignedUser)
                .Include(t => t.AssignedUnregUser)
                .Include(t => t.Booking)
                .Where(x => x.Booking.Id == bookingId)
                .ToListAsync();
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
    }
}
