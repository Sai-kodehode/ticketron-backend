using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnregUserRepository _unregUserRepository;

        public ParticipantController(IParticipantRepository participantRepository, IMapper mapper, IBookingRepository bookingRepository, IUserRepository userRepository, IUnregUserRepository unregUserRepository)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _unregUserRepository = unregUserRepository;
        }

        [HttpGet("{participantId}")]
        [ProducesResponseType(200, Type = typeof(Participant))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetParticipant(int participantId)
        {
            if (!_participantRepository.ParticipantExists(participantId))
                return NotFound();

            var participant = _mapper.Map<ParticipantDto>(_participantRepository.GetParticipant(participantId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(participant);
        }

        [HttpGet("booking/{bookingId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Participant>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetParticipants(int bookingId)
        {
            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var participants = _mapper.Map<List<ParticipantDto>>(_participantRepository.GetParticipants(bookingId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(participants);
        }

        [HttpPost("{bookingId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public IActionResult CreateParticipant(int bookingId, [FromBody] ParticipantDto newParticipant)
        {
            if (newParticipant == null)
                return BadRequest();

            if (newParticipant.UserId == null && newParticipant.UnregUserId == null)
                return BadRequest();

            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var participant = _mapper.Map<Participant>(newParticipant);
            participant.Booking = _bookingRepository.GetBooking(bookingId);

            if (newParticipant.IsUser && newParticipant.UserId.HasValue)
            {
                var user = _userRepository.GetUser(newParticipant.UserId.Value);
                if (user == null)
                    return NotFound();

                participant.User = user;
            }
            else if (newParticipant.IsUser == false && newParticipant.UnregUserId.HasValue)
            {
                var unregUser = _unregUserRepository.GetUnregUser(newParticipant.UnregUserId.Value);
                if (unregUser == null)
                    return NotFound();

                participant.UnregUser = unregUser;
            }
            else return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_participantRepository.CreateParticipant(participant))
                return BadRequest();

            return StatusCode(201);
        }

        [HttpDelete("{participantId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteParticipant(int participantId)
        {
            if (!_participantRepository.ParticipantExists(participantId))
                return NotFound();

            var participant = _participantRepository.GetParticipant(participantId);

            if (!_participantRepository.DeleteParticipant(participant))
                return BadRequest();

            return NoContent();
        }
    }
}
