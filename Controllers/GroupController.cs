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

        public GroupController(IGroupRepository groupRepository, IMapper mapper, IUserRepository userRepository, IUserContextService userContextService)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _userContextService = userContextService;
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

            return Ok(group);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Group>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGroups(Guid userId)
        {
            var groupsMap = _mapper.Map<List<GroupResponseDto>>(await _groupRepository.GetGroupsAsync(userId));

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

            var user = await _userRepository.GetUserByIdAsync(currentUserId);
            if (user == null)
                return NotFound("User not found");

            var groupMap = _mapper.Map<Group>(newGroup);
            groupMap.User = user;

            if (!await _groupRepository.CreateGroupAsync(groupMap))
                return StatusCode(500, "Error creating the group");

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

            if (!await _groupRepository.UpdateGroupAsync(groupMap))
                return StatusCode(500, "Error updating the group.");

            return NoContent();
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
                return StatusCode(500);

            return NoContent();
        }
    }
}
