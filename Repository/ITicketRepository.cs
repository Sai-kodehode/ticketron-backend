using Ticketron.Models;

namespace Ticketron.Repository
{
    public interface ITicketRepository
    {

       ICollection<Ticket> GetTickets();
      
   
   
    }
}
