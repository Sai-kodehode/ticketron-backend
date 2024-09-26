using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
       

       public TicketController(ITicketRepository ticketRepository, IMapper imapper, IBookingRepository bookingRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = imapper;
            _bookingRepository= bookingRepository;

        }
        [HttpGet("{ticketId}")]
        public IActionResult GetTicket(int ticketId)
        {
            if(!_ticketRepository.TicketExists(ticketId)) 
                return NotFound();
            var ticket=_mapper.Map<TicketDto>(_ticketRepository.GetTicket(ticketId));

            if (!ModelState.IsValid)
            
                return BadRequest();
            return Ok(ticket);       

        }

        [HttpGet("booking/{bookingId}")]
        public IActionResult GetTickets(int bookingId)
        {
            var tickets=_mapper.Map<List<TicketDto>>(_ticketRepository.GetTickets(bookingId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(tickets);
        }

        [HttpPost("{bookingId}")]
        public IActionResult CreateTicket(int bookingId, [FromBody] TicketDto newTicket)
        {

            if(newTicket==null)
                return BadRequest();
            
            if(!ModelState.IsValid)
                return BadRequest();

            var ticketMap = _mapper.Map<Ticket>(newTicket);
            ticketMap.Booking=_bookingRepository.GetBooking(bookingId);

            if (!_ticketRepository.CreateTicket(ticketMap))
            {
                return StatusCode(500);
            }
            return StatusCode(201);
        }

        [HttpPut("{ticketId}")]
        public IActionResult UpdateTicket(int ticketId, [FromBody] TicketDto updateTicket)
        {

            if(UpdateTicket==null)
                return BadRequest();
            if (!_ticketRepository.TicketExists(ticketId))
                return NotFound();

            var existingTicket= _ticketRepository.GetTicket(ticketId);
           

            var ticketMap= _mapper.Map<Ticket>(updateTicket);

            ticketMap.Id = ticketId;

            if (!_ticketRepository.UpdateTicket(ticketMap))
                return StatusCode(500);
            return NoContent();
        }
        [HttpDelete("{ticketId}")]
        public IActionResult DeleteTicket(int ticketId)
        {

            var existingUser=_ticketRepository.GetTicket(ticketId);

            if (existingUser == null)
                return NotFound();
            if (!_ticketRepository.DeleteTicket(existingUser))
                return StatusCode(500);
            return NoContent();
        }





    }
}
