using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Dto;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper, IUserRepository userRepository, DataContext context)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _context = context;
        }

        [HttpGet("{bookingId}")]
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
        public IActionResult GetBookings(int userId)
        {
            var bookings = _mapper.Map<List<BookingDto>>(_bookingRepository.GetBookings(userId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(bookings);
        }

        [HttpPost("{userId}")]
        public IActionResult CreateBooking(int userId, [FromBody] BookingDto newBooking)
        {
            if (newBooking == null)
                return BadRequest();

            var bookingMap = _mapper.Map<Booking>(newBooking);
            bookingMap.User = _userRepository.GetUser(userId);

            if (!_bookingRepository.CreateBooking(bookingMap))
                return BadRequest();

            return Ok();
        }

        [HttpPut("{bookingId}")]
        public IActionResult UpdateBooking(int bookingId, [FromBody] BookingDto updateBooking)
        {
            if (updateBooking == null)
                return BadRequest();

            var existingBooking = _bookingRepository.GetBooking(bookingId);

            if (existingBooking == null)
                return NotFound();

            _context.Entry(existingBooking).State = EntityState.Detached;

            var bookingMap = _mapper.Map<Booking>(updateBooking);
            bookingMap.Id = bookingId;

            if (!_bookingRepository.UpdateBooking(bookingMap))
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{bookingId}")]
        public IActionResult DeleteBooking(int bookingId)
        {
            var booking = _bookingRepository.GetBooking(bookingId);

            if (booking == null)
                return NotFound();

            if (!_bookingRepository.DeleteBooking(booking))
                return BadRequest();

            return Ok();
        }
    }
}
