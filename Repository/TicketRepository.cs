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

        public bool CreateTicket(Ticket ticket)
        {
            _context.Add(ticket);
            return Save();
        }

        public bool DeleteTicket(Ticket ticket)
        {
            _context.Remove(ticket);
            return Save();
        }

        public Ticket GetTicket(int ticketId)
        {
            return _context.Tickets.Where(x => x.Id == ticketId).FirstOrDefault();
        }

        public ICollection<Ticket> GetTickets(User user)
        {
            throw new NotImplementedException();
        }

        public ICollection<Ticket> GetTickets(int bookingId)
        {
            return _context.Tickets.Where(x => x.Booking.Id == bookingId).ToList();
        }



        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
        public bool TicketExists(int ticketId)
        {
            return _context.Tickets.Any(x => x.Id == ticketId);
        }

        public bool UpdateTicket(Ticket ticket)
        {

            var existingTicket = _context.Tickets.Find(ticket.Id);
            if (existingTicket == null)
                return false;

            _context.Entry(existingTicket).State = EntityState.Detached;
            _context.Update(ticket);
            return Save();    
           
        }
    }
}
