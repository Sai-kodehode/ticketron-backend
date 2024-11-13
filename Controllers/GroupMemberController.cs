using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto.GroupMemberDto;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GroupMemberController : Controller
    {
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnregUserRepository _unregUserRepository;

        public GroupMemberController(IGroupMemberRepository groupMemberRepository, IMapper mapper, IGroupRepository groupRepository, IUserRepository userRepository, IUnregUserRepository unregUserRepository)
        {
            _groupMemberRepository = groupMemberRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _unregUserRepository = unregUserRepository;
        }

        [HttpGet("{groupMemberId}")]
        [ProducesResponseType(200, Type = typeof(GroupMemberDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGroupMember(Guid groupMemberId)
        {
            var groupMember = await _groupMemberRepository.GetGroupMemberAsync(groupMemberId);

            if (groupMember == null)
                return NotFound("GroupMember not found.");

            if (groupMember.User == null && groupMember.UnregUser == null)
                return NotFound("User or UnregUser not found.");

            var groupMemberResponse = new GroupMemberDto
            {
                GroupId = groupMember.Group.Id,
                UserId = groupMember.User?.Id,
                UnregUserId = groupMember.UnregUser?.Id,
                Name = groupMember.User?.Name ?? groupMember.UnregUser?.Name ?? "Unkown",
            };

            return Ok(groupMemberResponse);
        }

        [HttpGet("group/{groupId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GroupMemberDto>))]
        public async Task<IActionResult> GetGroupMembers(Guid groupId)
        {
            var groupMembers = _mapper.Map<List<GroupMemberDto>>(await _groupMemberRepository.GetGroupMembersAsync(groupId));

            return Ok(groupMembers);
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateGroupMember([FromBody] GroupMemberDto newGroupMember)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (newGroupMember == null)
                return BadRequest();

            if (newGroupMember.UserId == null && newGroupMember.UnregUserId == null)
                return BadRequest("Both User and UnregUser cannot be set at the same time.");

            if (newGroupMember.UserId != null && newGroupMember.UnregUserId != null)
                return BadRequest("Either User or UnregUser must be set.");

            var groupMap = _mapper.Map<GroupMember>(newGroupMember);

            if (newGroupMember.UserId != null)
            {
                var user = await _userRepository.GetUserByIdAsync(newGroupMember.UserId.Value);
                if (user == null)
                    return NotFound("User not found.");

                groupMap.User = user;
            }
            else if (newGroupMember.UnregUserId != null)
            {
                var unregUser = await _unregUserRepository.GetUnregUserAsync(newGroupMember.UnregUserId.Value);
                if (unregUser == null)
                    return NotFound("UnregUser not found.");

                groupMap.UnregUser = unregUser;
            }

            var group = await _groupRepository.GetGroupAsync(newGroupMember.GroupId);
            if (group == null)
                return NotFound("Group not found.");

            groupMap.Group = group;

            if (!await _groupMemberRepository.CreateGroupMemberAsync(groupMap))
                return StatusCode(500);

            return StatusCode(201, groupMap);
        }

        [HttpDelete("{groupMemberId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteGroupMember(Guid groupMemberId)
        {
            var existingGroupMember = await _groupMemberRepository.GetGroupMemberAsync(groupMemberId);

            if (existingGroupMember == null)
                return NotFound();

            if (!await _groupMemberRepository.DeleteGroupMemberAsync(existingGroupMember))
                return StatusCode(500);

            return NoContent();
        }

    }
}
