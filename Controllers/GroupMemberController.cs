using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto;
using Ticketron.Interfaces;
using Ticketron.Models;
using Ticketron.Repository;

namespace Ticketron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GroupMemberController : Controller
    {
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GroupMemberController(IGroupMemberRepository groupMemberRepository, IMapper mapper, IGroupRepository groupRepository)
        {
            _groupMemberRepository = groupMemberRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        [HttpGet("{groupMemberId}")]
        public IActionResult GetGroupMember(int groupMemberId)
        {
            if (!_groupMemberRepository.GroupMemberExists(groupMemberId))
                return NotFound();
            var groupMember = _mapper.Map<GroupMemberDto>(_groupMemberRepository.GetGroupMember(groupMemberId));

            if (!ModelState.IsValid)

                return BadRequest();
            return Ok(groupMember);

        }

        [HttpGet("group/{groupId}")]

        public IActionResult GetGroupMembers(int groupId)
        {

            var groupMembers=_mapper.Map<List<GroupMemberDto>>(_groupMemberRepository.GetGroupMembers(groupId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(groupMembers);
        }

        [HttpPost("group/{groupId}")]
        public IActionResult CreateGroupMember(int groupId, [FromBody] GroupMemberDto newGroupMember)
        {

            if (newGroupMember == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var groupMap = _mapper.Map<GroupMember>(newGroupMember);
            groupMap.Group = _groupRepository.GetGroup(groupId);

            if (!_groupMemberRepository.CreateGroupMember(groupMap))
            {
                return StatusCode(500);
            }
            return StatusCode(201);
        }



        [HttpDelete("{groupMemberId}")]
        public IActionResult DeleteGroupmember(int groupMemberId)
        {

            var existingGroupMember = _groupMemberRepository.GetGroupMember(groupMemberId);

            if (existingGroupMember == null)
                return NotFound();
            if (!_groupMemberRepository.DeleteGroupMember(existingGroupMember))
                return StatusCode(500);
            return NoContent();
        }

    }
}
