using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public TicketRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
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
            var currentUserId = _userContextService.GetUserObjectId();

            return await _context.Tickets
                .Include(t => t.PurchasedBy)
                .Include(t => t.AssignedUser)
                .Include(t => t.AssignedUnregUser)
                    .ThenInclude(x => x.CreatedBy)
                .Include(t => t.Booking)
                .FirstOrDefaultAsync(t => t.Id == ticketId &&
                ((t.AssignedUser != null && t.AssignedUser.Id == currentUserId) ||
                t.Booking.CreatedById == currentUserId));
        }

        public async Task<ICollection<Ticket>> GetTicketsAsync(Guid bookingId)
        {
            var currentUserId = _userContextService.GetUserObjectId();

            return await _context.Tickets
                .Include(t => t.PurchasedBy)
                .Include(t => t.AssignedUser)
                .Include(t => t.AssignedUnregUser)
                .Include(t => t.Booking)
                .Where(t => t.Booking.Id == bookingId &&
                ((t.AssignedUser != null && t.AssignedUser.Id == currentUserId) ||
                t.Booking.CreatedById == currentUserId))
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
