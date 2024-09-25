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
        public IActionResult GetParticipant(int participantId)
        {
            if (!_participantRepository.ParticipantExists(participantId))
                return NotFound();

            var participant = _participantRepository.GetParticipant(participantId);

            var participantDto = new Participant
            {
                AddedBy = participant.AddedBy,
                Booking = _bookingRepository.GetBooking(participant.Booking.Id),
                UserId = ,
            };



            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(participant);
        }

        [HttpGet("booking/{bookingId}")]
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
        public IActionResult CreateParticipant(int bookingId, [FromBody] ParticipantDto newParticipant)
        {
            if (newParticipant == null)
                return BadRequest();

            if (newParticipant.UserId == null && newParticipant.UnregUserId == null)
                return BadRequest();

            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var participantMap = new Participant
            {
                AddedBy = newParticipant.AddedBy,
                Booking = _bookingRepository.GetBooking(bookingId),
                IsUser = newParticipant.IsUser
            };

            if (newParticipant.IsUser == true && newParticipant.UserId != null)
            {
                participantMap.User = _userRepository.GetUser((int)newParticipant.UserId);
            }
            else if (newParticipant.IsUser == false && newParticipant.UnregUserId != null)
            {
                participantMap.UnregUser = _unregUserRepository.GetUnregUser((int)newParticipant.UnregUserId);
            }
            else return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_participantRepository.CreateParticipant(participantMap))
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{participantId}")]
        public IActionResult DeleteParticipant(int participantId)
        {
            if (!_participantRepository.ParticipantExists(participantId))
                return NotFound();

            var participant = _participantRepository.GetParticipant(participantId);

            if (!_participantRepository.DeleteParticipant(participant))
                return BadRequest();

            return Ok();
        }
    }
}
