using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto.BookingDto;
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
        private readonly IUnregUserRepository _unregUserRepository;
        private readonly IGroupRepository _groupRepostitory;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper, IUserRepository userRepository, IUserContextService userContextService, ILogger<BookingController> logger, IUnregUserRepository unregUserRepository, IGroupRepository groupRepostitory)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _userContextService = userContextService;
            _logger = logger;
            _unregUserRepository = unregUserRepository;
            _groupRepostitory = groupRepostitory;
        }

        [HttpGet("{bookingId}")]
        [ProducesResponseType(200, Type = typeof(BookingResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> GetBooking(Guid bookingId)
        {
            _logger.LogInformation("Getting booking with id: {bookingId}", bookingId);
            var booking = await _bookingRepository.GetBookingAsync(bookingId);
            if (booking == null)
            {
                _logger.LogWarning("Booking with id: {bookingId} not found", bookingId);
                return NotFound("Booking not found");
            }

            _logger.LogTrace("Returning booking with id: {bookingId}", bookingId);
            return Ok(_mapper.Map<BookingResponseDto>(booking));
        }

        [HttpGet("summary/{bookingId}")]
        [ProducesResponseType(200, Type = typeof(BookingSummaryResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBookingSummary(Guid bookingId)
        {
            var booking = await _bookingRepository.GetBookingAsync(bookingId);
            if (booking == null)
                return NotFound("Booking not found");

            var bookingMap = _mapper.Map<BookingSummaryResponseDto>(booking);

            return Ok(bookingMap);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingResponseDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();

            return Ok(_mapper.Map<List<BookingResponseDto>>(bookings));
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

            var currentUser = await _userRepository.GetUserByIdAsync(objectId);

            if (currentUser == null)
                return Problem();

            var booking = _mapper.Map<Booking>(newBooking);

            booking.CreatedBy = currentUser;
            booking.CreatedById = currentUser.Id;
            booking.Users = await _userRepository.GetUsersByIdsAsync(newBooking.UserIds);
            booking.UnregUsers = await _unregUserRepository.GetUnregUsersByIdsAsync(newBooking.UnregUserIds);
            booking.Groups = await _groupRepostitory.GetGroupsByIdsAsync(newBooking.GroupIds);

            if (!await _bookingRepository.CreateBookingAsync(booking))
                return Problem();

            var createdBookingDto = _mapper.Map<BookingResponseDto>(booking);

            return Ok(createdBookingDto);
        }

        [HttpPut("update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingUpdateDto updatedBooking)
        {
            if (updatedBooking == null)
                return BadRequest("Missing data.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var existingBooking = await _bookingRepository.GetBookingAsync(updatedBooking.Id);
            if (existingBooking == null)
                return NotFound("Booking not found.");

            Guid currentUserId;

            try
            {
                currentUserId = _userContextService.GetUserObjectId();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

            if (existingBooking.CreatedById != currentUserId)
                return Unauthorized("You are not authorized to update this booking.");

            var bookingMap = _mapper.Map(updatedBooking, existingBooking);

            if (updatedBooking.UserIds != null)
                bookingMap.Users = await _userRepository.GetUsersByIdsAsync(updatedBooking.UserIds);

            if (updatedBooking.UnregUserIds != null)
                bookingMap.UnregUsers = await _unregUserRepository.GetUnregUsersByIdsAsync(updatedBooking.UnregUserIds);

            if (updatedBooking.GroupIds != null)
                bookingMap.Groups = await _groupRepostitory.GetGroupsByIdsAsync(updatedBooking.GroupIds);

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

            Guid currentUserId;

            try
            {
                currentUserId = _userContextService.GetUserObjectId();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

            if (booking.CreatedById != currentUserId)
                return Unauthorized("You are not authorized to delete this booking.");

            if (!await _bookingRepository.DeleteBookingAsync(booking))
                return BadRequest();

            return NoContent();
        }
    }
}
