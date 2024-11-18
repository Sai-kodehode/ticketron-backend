using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto.GroupDto.GroupDto;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;
        private readonly IUnregUserRepository _unregUserRepository;

        public GroupController(IGroupRepository groupRepository, IMapper mapper, IUserRepository userRepository, IUserContextService userContextService, IUnregUserRepository unregUserRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _userContextService = userContextService;
            _unregUserRepository = unregUserRepository;
        }

        [HttpGet("{groupId}")]
        [ProducesResponseType(200, Type = typeof(GroupResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGroup(Guid groupId)
        {
            var group = await _groupRepository.GetGroupAsync(groupId);

            if (group == null)
                return NotFound();

            var groupMap = _mapper.Map<GroupResponseDto>(group);

            return Ok(groupMap);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Group>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGroups(Guid userId)
        {
            var groupsMap = _mapper.Map<List<GroupResponseDto>>(await _groupRepository.GetGroupsByUserIdAsync(userId));

            return Ok(groupsMap);
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateGroup([FromBody] GroupCreateDto newGroup)
        {
            if (newGroup == null)
                return BadRequest("Missing data");

            if (!ModelState.IsValid)
                return BadRequest();

            Guid currentUserId;
            try
            {
                currentUserId = _userContextService.GetUserObjectId();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            };

            var currentUser = await _userRepository.GetUserByIdAsync(currentUserId);
            if (currentUser == null)
                return NotFound("User not found");

            var groupMap = _mapper.Map<Group>(newGroup);
            groupMap.CreatedBy = currentUser;
            groupMap.CreatedById = currentUser.Id;
            groupMap.Users = await _userRepository.GetUsersByIdsAsync(newGroup.UserIds);
            groupMap.UnregUsers = await _unregUserRepository.GetUnregUsersByIdsAsync(newGroup.UnregUserIds);

            if (!await _groupRepository.CreateGroupAsync(groupMap))
                return Problem();

            return NoContent();
        }

        [HttpPut("update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateGroup([FromBody] GroupUpdateDto updatedGroup)
        {
            if (updatedGroup == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var existingGroup = await _groupRepository.GetGroupAsync(updatedGroup.Id);
            if (existingGroup == null)
                return NotFound();

            var groupMap = _mapper.Map(updatedGroup, existingGroup);
            groupMap.Users = await _userRepository.GetUsersByIdsAsync(updatedGroup.UserIds);
            groupMap.UnregUsers = await _unregUserRepository.GetUnregUsersByIdsAsync(updatedGroup.UnregUserIds);

            if (!await _groupRepository.SaveAsync())
                return Problem();

            return Ok(_mapper.Map<GroupResponseDto>(groupMap));
        }

        [HttpDelete("{groupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteGroup(Guid groupId)
        {
            var existingGroup = await _groupRepository.GetGroupAsync(groupId);
            if (existingGroup == null)
                return NotFound();

            if (!await _groupRepository.DeleteGroupAsync(existingGroup))
                return Problem();

            return NoContent();
        }
    }
}
