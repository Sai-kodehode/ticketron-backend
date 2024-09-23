using Microsoft.AspNetCore.Mvc;
using Ticketron.Models;
using Ticketron.Repository;

namespace Ticketron.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class TicketsController:Controller
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketsController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<Ticket>))]
            public IActionResult GetTickets()
        {
            var tickets= _ticketRepository.GetTickets();
        
            if(!ModelState.IsValid) 
                return BadRequest ();
            return Ok(tickets);
        }

    }
}
