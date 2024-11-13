using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto.BookingDto.BookingDto;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;
        private readonly ILogger<BookingController> _logger;
        public BookingController(IBookingRepository bookingRepository, IMapper mapper, IUserRepository userRepository, IUserContextService userContextService, ILogger<BookingController> logger
            )
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _userContextService = userContextService;
            _logger = logger;
        }

        [HttpGet("{bookingId}")]
        [ProducesResponseType(200, Type = typeof(BookingResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> GetBooking(Guid bookingId)
        {
            _logger.LogInformation("Getting booking with id: {bookingId}", bookingId);
            var booking = _mapper.Map<BookingResponseDto>(await _bookingRepository.GetBookingAsync(bookingId));

            if (booking == null)
            {
                _logger.LogWarning("Booking with id: {bookingId} not found", bookingId);
                return NotFound();
            }

            _logger.LogTrace("Returning booking with id: {bookingId}", bookingId);
            return Ok(booking);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingResponseDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBookings(Guid userId)
        {
            var bookings = _mapper.Map<List<BookingResponseDto>>(await _bookingRepository.GetBookingsAsync(userId));

            return Ok(bookings);
        }

        [HttpPost("create")]
        [ProducesResponseType(200, Type = typeof(BookingResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto newBooking)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (newBooking == null)
                return BadRequest();

            Guid objectId;
            try
            {
                objectId = _userContextService.GetUserObjectId();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

            var user = await _userRepository.GetUserByIdAsync(objectId);

            if (user == null)
                return NotFound("User not found");

            var booking = _mapper.Map<Booking>(newBooking);

            booking.User = user;

            if (!await _bookingRepository.CreateBookingAsync(booking))
                return StatusCode(500, "Could not create new booking");

            var createdBookingDto = _mapper.Map<BookingResponseDto>(booking);

            return Ok(createdBookingDto);
        }

        [HttpPut("update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingUpdateDto updateBooking)
        {
            if (updateBooking == null)
                return BadRequest("Missing data.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var existingBooking = await _bookingRepository.GetBookingAsync(updateBooking.Id);
            if (existingBooking == null)
                return NotFound("Booking not found.");

            var bookingMap = _mapper.Map(updateBooking, existingBooking);

            if (!await _bookingRepository.SaveAsync())
                return Problem("Error updating the booking.");

            return Ok(_mapper.Map<BookingResponseDto>(await _bookingRepository.GetBookingAsync(existingBooking.Id)));
        }

        [HttpDelete("{bookingId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBooking(Guid bookingId)
        {
            var booking = await _bookingRepository.GetBookingAsync(bookingId);

            if (booking == null)
                return NotFound();

            if (!await _bookingRepository.DeleteBookingAsync(booking))
                return BadRequest();

            return NoContent();
        }
    }
}
