using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto.TicketDto;
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
        private readonly IBookingRepository _bookingRepository;
        private readonly IBlobService _blobService;


        public TicketController(ITicketRepository ticketRepository, IMapper imapper, IBookingRepository bookingRepository, IBlobService blobService)
        {
            _ticketRepository = ticketRepository;
            _mapper = imapper;
            _bookingRepository = bookingRepository;
            _blobService = blobService;
        }
        [HttpGet("{ticketId}")]
        [ProducesResponseType(200, Type = typeof(TicketDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetTicket(int ticketId)
        {
            if (!_ticketRepository.TicketExists(ticketId))
                return NotFound();
            var ticket = _mapper.Map<TicketDto>(_ticketRepository.GetTicket(ticketId));

            if (!ModelState.IsValid)

                return BadRequest();
            return Ok(ticket);

        }

        [HttpGet("booking/{bookingId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TicketDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTickets(int bookingId)
        {
            var tickets = _mapper.Map<List<TicketDto>>(_ticketRepository.GetTickets(bookingId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(tickets);
        }

        [HttpPost("create")]
        [ProducesResponseType(201, Type = typeof(TicketDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto newTicket)
        {
            if (newTicket == null)
                return BadRequest();

            var booking = _bookingRepository.GetBooking(newTicket.BookingId);
            if (booking == null)
                return NotFound();

            string? blobName = null;
            if (newTicket.Image != null)
            {
                blobName = await _blobService.UploadImageAsync(newTicket.Image, newTicket.BookingId);
            }

            var ticket = _mapper.Map<Ticket>(newTicket);
            ticket.Booking = booking;
            ticket.BlobName = blobName;

            if (!_ticketRepository.CreateTicket(ticket))
                return StatusCode(500);

            var createdTicketDto = _mapper.Map<TicketDto>(ticket);
            return Ok(createdTicketDto);
        }

        [HttpPut("{ticketId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTicket(int ticketId, [FromBody] TicketUpdateDto updateTicket)
        {

            if (UpdateTicket == null)
                return BadRequest();

            var existingTicket = _ticketRepository.GetTicket(ticketId);


            if (existingTicket == null)
                return NotFound("Booking not found");


            var ticketMap = _mapper.Map(updateTicket, existingTicket);

            ticketMap.Id = ticketId;

            if (!_ticketRepository.UpdateTicket(ticketMap))
                return StatusCode(500);
            return NoContent();
        }

        [HttpDelete("{ticketId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTicket(int ticketId)
        {

            var ticket = _ticketRepository.GetTicket(ticketId);

            if (ticket == null)
                return NotFound();
            if (!_ticketRepository.DeleteTicket(ticket))
                return StatusCode(500);
            return NoContent();
        }
    }
}
