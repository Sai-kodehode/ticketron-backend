//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Ticketron.Dto.ParticipantDto;
//using Ticketron.Interfaces;
//using Ticketron.Models;

//namespace Ticketron.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ParticipantController : ControllerBase
//    {
//        private readonly IParticipantRepository _participantRepository;
//        private readonly IMapper _mapper;
//        private readonly IBookingRepository _bookingRepository;
//        private readonly IUserRepository _userRepository;
//        private readonly IUnregUserRepository _unregUserRepository;
//        private readonly IUserContextService _userContextService;

//        public ParticipantController(IParticipantRepository participantRepository, IMapper mapper, IBookingRepository bookingRepository, IUserRepository userRepository, IUnregUserRepository unregUserRepository, IUserContextService userContextService)
//        {
//            _participantRepository = participantRepository;
//            _mapper = mapper;
//            _bookingRepository = bookingRepository;
//            _userRepository = userRepository;
//            _unregUserRepository = unregUserRepository;
//            _userContextService = userContextService;
//        }

//        [HttpGet("{participantId}")]
//        [ProducesResponseType(200, Type = typeof(ParticipantResponseDto))]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(404)]
//        public async Task<IActionResult> GetParticipantAsync(Guid participantId)
//        {
//            var participant = await _participantRepository.GetParticipantAsync(participantId);

//            if (participant == null)
//                return NotFound();

//            var participantMap = _mapper.Map<ParticipantResponseDto>(participant);

//            return Ok(participantMap);
//        }

//        [HttpGet("booking/{bookingId}")]
//        [ProducesResponseType(200, Type = typeof(IEnumerable<ParticipantResponseDto>))]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(404)]
//        public async Task<IActionResult> GetParticipantsAsync(Guid bookingId)
//        {
//            if (!await _bookingRepository.BookingExistsAsync(bookingId))
//                return NotFound();

//            var participantsMap = _mapper.Map<List<ParticipantResponseDto>>(await _participantRepository.GetParticipantsAsync(bookingId));

//            if (!ModelState.IsValid)
//                return BadRequest();

//            return Ok(participantsMap);
//        }

//        [HttpPost("create")]
//        [ProducesResponseType(201)]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> CreateParticipantAsync([FromBody] ParticipantCreateDto newParticipant)
//        {
//            if (newParticipant == null)
//                return BadRequest();

//            if (!ModelState.IsValid)
//                return BadRequest();

//            if (newParticipant.UserId == null && newParticipant.UnregUserId == null)
//                return BadRequest("Either UserId or UnregUserId must be included");

//            if (newParticipant.UserId != null && newParticipant.UnregUserId != null)
//                return BadRequest("Only one of UserId or UnregUserId can be included");

//            if (!await _bookingRepository.BookingExistsAsync(newParticipant.BookingId))
//                return NotFound();

//            var participantMap = _mapper.Map<Participant>(newParticipant);

//            var booking = await _bookingRepository.GetBookingAsync(newParticipant.BookingId);
//            if (booking == null)
//                return NotFound("Booking not found");

//            participantMap.Booking = booking;

//            if (newParticipant.UserId != null)
//            {
//                var user = await _userRepository.GetUserByIdAsync(newParticipant.UserId.Value);
//                if (user == null)
//                    return NotFound();

//                participantMap.User = user;
//            }
//            else if (newParticipant.UnregUserId != null)
//            {
//                var unregUser = await _unregUserRepository.GetUnregUserAsync(newParticipant.UnregUserId.Value);
//                if (unregUser == null)
//                    return NotFound();

//                participantMap.UnregUser = unregUser;
//            }
//            else return StatusCode(500);

//            Guid currentUserId;
//            try
//            {
//                currentUserId = _userContextService.GetUserObjectId();
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                return Unauthorized(ex.Message);
//            }
//            participantMap.CreatedBy = currentUserId;

//            if (!await _participantRepository.CreateParticipantAsync(participantMap))
//                return BadRequest();

//            return Ok(_mapper.Map<ParticipantResponseDto>(participantMap));
//        }

//        [HttpDelete("{participantId}")]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> DeleteParticipant(Guid participantId)
//        {
//            var participant = await _participantRepository.GetParticipantAsync(participantId);

//            if (participant == null)
//                return NotFound();

//            if (!await _participantRepository.DeleteParticipantAsync(participant))
//                return BadRequest();

//            return NoContent();
//        }
//    }
//}
