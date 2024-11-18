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
        private readonly IUserContextService _userContextService;
        private readonly IUnregUserRepository _unregUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBlobService _blobService;

        public TicketController(ITicketRepository ticketRepository, IMapper imapper, IBookingRepository bookingRepository, IUserContextService userContextService, IUserRepository userRepository, IBlobService blobService, IUnregUserRepository unregUserRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = imapper;
            _bookingRepository = bookingRepository;
            _userContextService = userContextService;
            _userRepository = userRepository;
            _blobService = blobService;
            _unregUserRepository = unregUserRepository;
        }

        [HttpGet("{ticketId}")]
        [ProducesResponseType(200, Type = typeof(TicketResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTicket(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetTicketAsync(ticketId);

            if (ticket == null)
                return NotFound("Ticket not found");

            return Ok(_mapper.Map<TicketResponseDto>(ticket));
        }

        [HttpGet("booking/{bookingId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TicketResponseDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTickets(Guid bookingId)
        {
            var tickets = _mapper.Map<List<TicketResponseDto>>(await _ticketRepository.GetTicketsAsync(bookingId));

            return Ok(tickets);
        }

        [HttpPost("create")]
        [ProducesResponseType(201, Type = typeof(TicketResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto newTicket)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (newTicket == null)
                return BadRequest();

            if (newTicket.AssignedUserId != null && newTicket.AssignedUnregUserId != null)
                return BadRequest("Cannot assign ticket to both registered and unregistered user");

            if (newTicket.AssignedUserId == null && newTicket.AssignedUnregUserId == null)
                return BadRequest("Ticket must be assigned to a user");

            var booking = await _bookingRepository.GetBookingAsync(newTicket.BookingId);
            if (booking == null)
                return NotFound("Booking not found");

            string? imageUrl = null;

            if (!string.IsNullOrEmpty(newTicket.ImageUrl))
            {

                imageUrl = newTicket.ImageUrl;
            }
            //if (newTicket.Image != null)
            //{
            //    blobName = await _blobService.UploadImageAsync(newTicket.Image, newTicket.BookingId);
            //}

            var ticketMap = _mapper.Map<Ticket>(newTicket);

            if (newTicket.AssignedUserId != null)
            {
                var assignedUser = await _userRepository.GetUserByIdAsync(newTicket.AssignedUserId.Value);
                if (assignedUser == null)
                    return NotFound("User not found");

                ticketMap.AssignedUser = assignedUser;
            }

            if (newTicket.AssignedUnregUserId != null)
            {
                var assignedUnregUser = await _unregUserRepository.GetUnregUserAsync(newTicket.AssignedUnregUserId.Value);
                if (assignedUnregUser == null)
                    return NotFound("Unregistered user not found");

                ticketMap.AssignedUnregUser = assignedUnregUser;
            }

            if (newTicket.PurchasedBy != null)
            {
                var purchasedBy = await _userRepository.GetUserByIdAsync(newTicket.PurchasedBy.Value);
                if (purchasedBy == null)
                    return NotFound("User not found");

                ticketMap.PurchasedBy = purchasedBy;
            }

            ticketMap.Booking = booking;
            ticketMap.ImageUrl = imageUrl;

            if (!await _ticketRepository.CreateTicketAsync(ticketMap))
                return Problem();

            return Ok(_mapper.Map<TicketResponseDto>(ticketMap));
        }

        [HttpPut("update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateTicket([FromBody] TicketUpdateDto updatedTicket)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (UpdateTicket == null)
                return BadRequest();

            if (updatedTicket.AssignedUserId != null && updatedTicket.AssignedUnregUserId != null)
                return BadRequest("Cannot assign ticket to both registered and unregistered user");

            if (updatedTicket.AssignedUserId == null && updatedTicket.AssignedUnregUserId == null)
                return BadRequest("Ticket must be assigned to a user");

            var existingTicket = await _ticketRepository.GetTicketAsync(updatedTicket.Id);
            if (existingTicket == null)
                return NotFound("Ticket not found");

            var ticketMap = _mapper.Map(updatedTicket, existingTicket);

            if (updatedTicket.AssignedUserId != null)
            {
                var assignedUser = await _userRepository.GetUserByIdAsync(updatedTicket.AssignedUserId.Value);
                if (assignedUser == null)
                    return NotFound("User not found");

                ticketMap.AssignedUser = assignedUser;
            }

            if (updatedTicket.AssignedUnregUserId != null)
            {
                var assignedUnregUser = await _unregUserRepository.GetUnregUserAsync(updatedTicket.AssignedUnregUserId.Value);
                if (assignedUnregUser == null)
                    return NotFound("Unregistered user not found");

                ticketMap.AssignedUnregUser = assignedUnregUser;
            }

            if (updatedTicket.PurchasedBy != null)
            {
                var purchasedBy = await _userRepository.GetUserByIdAsync(updatedTicket.PurchasedBy.Value);
                if (purchasedBy == null)
                    return NotFound("User not found");

                ticketMap.PurchasedBy = purchasedBy;
            }

            if (!await _ticketRepository.SaveAsync())
                return Problem("Error updating ticket");

            return Ok(_mapper.Map<TicketResponseDto>(await _ticketRepository.GetTicketAsync(existingTicket.Id)));
        }

        [HttpDelete("{ticketId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteTicket(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetTicketAsync(ticketId);

            if (ticket == null)
                return NotFound();

            if (!string.IsNullOrEmpty(ticket.ImageUrl))
                if (!await _blobService.DeleteImage(ticket.ImageUrl))
                    return Problem();

            if (!await _ticketRepository.DeleteTicketAsync(ticket))
                return Problem();

            return NoContent();
        }
    }
}
