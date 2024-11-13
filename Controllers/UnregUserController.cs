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

            var unregUsersMap = _mapper.Map<List<UnregUserResponseDto>>(_unregUserRepository.GetUnregUsersByUserIdAsync(userId));

            return Ok(unregUsersMap);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUnregUser(Guid userId, [FromBody] UnregUserCreateDto newUnregUser)
        {
            if (!await _userRepository.UserExistsAsync(userId))
                return NotFound();

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

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _unregUserRepository.CreateUnregUserAsync(unregUserMap))
                return Problem();

            return Created();
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
