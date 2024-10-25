using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Data;
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
        public BookingController(IBookingRepository bookingRepository, IMapper mapper, IUserRepository userRepository, DataContext context)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _userRepository = userRepository;

        }

        [HttpGet("{bookingId}")]
        [ProducesResponseType(200, Type = typeof(BookingDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetBooking(int bookingId)
        {
            var booking = _mapper.Map<BookingDto>(_bookingRepository.GetBooking(bookingId));

            if (booking == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(booking);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookings(int userId)
        {
            var bookings = _mapper.Map<List<BookingDto>>(_bookingRepository.GetBookings(userId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(bookings);
        }

        [HttpPost("create")]
        [ProducesResponseType(200, Type = typeof(BookingDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateBooking([FromBody] BookingCreateDto newBooking)
        {
            if (newBooking == null)
                return BadRequest("Booking data is null.");

            var userId = newBooking.UserId;

            var user = _userRepository.GetUser(userId);
            if (user == null)
                return NotFound($"User with ID {userId} not found.");

            var booking = _mapper.Map<Booking>(newBooking);
            booking.User = user;

            if (!_bookingRepository.CreateBooking(booking))
                return StatusCode(500, "Error creating the booking.");

            var createdBookingDto = _mapper.Map<BookingDto>(booking);
            return Ok(createdBookingDto);
        }

        [HttpPut("{bookingId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateBooking(int bookingId, [FromBody] BookingUpdateDto updateBooking)
        {
            if (updateBooking == null)
                return BadRequest("Invalid data.");

            var existingBooking = _bookingRepository.GetBooking(bookingId);

            if (existingBooking == null)
                return NotFound("Booking not found.");

            var bookingMap = _mapper.Map(updateBooking, existingBooking);
            bookingMap.Id = bookingId;

            if (!_bookingRepository.UpdateBooking(bookingMap))
                return StatusCode(500, "Error updating the booking.");

            return NoContent();
        }

        [HttpDelete("{bookingId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBooking(int bookingId)
        {
            var booking = _bookingRepository.GetBooking(bookingId);

            if (booking == null)
                return NotFound();

            if (!_bookingRepository.DeleteBooking(booking))
                return BadRequest();

            return NoContent();
        }
    }
}
