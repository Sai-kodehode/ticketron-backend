using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto.UnregUserDto;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnregUserController : ControllerBase
    {
        private readonly IUnregUserRepository _unregUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public UnregUserController(IUnregUserRepository unregUserRepository, IUserRepository userRepository, IMapper imapper, IUserContextService userContextService)
        {
            _unregUserRepository = unregUserRepository;
            _userRepository = userRepository;
            _mapper = imapper;
            _userContextService = userContextService;
        }

        [HttpGet("{unregUserId}")]
        [ProducesResponseType(200, Type = typeof(UnregUserResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> GetUnregUser(Guid unregUserId)
        {
            var unregUser = await _unregUserRepository.GetUnregUserAsync(unregUserId);
            if (unregUser == null)
                return NotFound();

            var unregUserMap = _mapper.Map<UnregUserResponseDto>(unregUser);

            return Ok(unregUserMap);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UnregUserResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUnregUsersByUserId(Guid userId)
        {
            if (!await _userRepository.UserExistsAsync(userId))
                return NotFound("User not found");

            var unregUsers = await _unregUserRepository.GetUnregUsersByUserIdAsync(userId);

            return Ok(_mapper.Map<List<UnregUserResponseDto>>(unregUsers));
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUnregUser([FromBody] UnregUserCreateDto newUnregUser)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (newUnregUser == null)
                return BadRequest();

            var unregUserMap = _mapper.Map<UnregUser>(newUnregUser);

            Guid currentUserId;
            try
            {
                currentUserId = _userContextService.GetUserObjectId();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

            var user = await _userRepository.GetUserByIdAsync(currentUserId);
            if (user == null)
                return Problem();

            unregUserMap.CreatedBy = user;

            if (!await _unregUserRepository.CreateUnregUserAsync(unregUserMap))
                return Problem();

            return Ok(_mapper.Map<UnregUserResponseDto>(unregUserMap));
        }

        [HttpDelete("{unregUserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUnregUser(Guid unregUserId)
        {
            var existingUnregUser = await _unregUserRepository.GetUnregUserAsync(unregUserId);
            if (existingUnregUser == null)
                return NotFound();

            if (!await _unregUserRepository.DeleteUnregUserAsync(existingUnregUser))
                return Problem();

            return NoContent();
        }
    }
}
