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
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GroupController(IGroupRepository groupRepository, IMapper mapper, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("{groupId}")]
        [ProducesResponseType(200, Type = typeof(Group))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetGroup(int groupId)
        {
            if (!_groupRepository.GroupExists(groupId))
                return NotFound();

            var group = _mapper.Map<GroupDto>(_groupRepository.GetGroup(groupId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(group);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Group>))]
        [ProducesResponseType(400)]
        public IActionResult GetGroups(int userId)
        {
            var groups = _mapper.Map<List<GroupDto>>(_groupRepository.GetGroups(userId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(groups);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateGroup(int userId, [FromBody] GroupDto newGroup)
        {
            if (newGroup == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest();
            var groupMap = _mapper.Map<Group>(newGroup);
            groupMap.User = _userRepository.GetUser(userId);
            if (!_groupRepository.CreateGroup(groupMap))
            {

                return StatusCode(500);
            }
            return StatusCode(201);


        }


        [HttpPut("{groupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateGroup(int groupId, [FromBody] GroupDto updateGroup)
        {
            if(updateGroup==null)
                return BadRequest();
            if (!_groupRepository.GroupExists(groupId))
                return NotFound();
            var existingGroup=_groupRepository.GetGroup(groupId);
            var groupMap = _mapper.Map<Group>(updateGroup);

            groupMap.Id = groupId;
            if (!_groupRepository.UpdateGroup(groupMap))
                return StatusCode(500);
            return NoContent();
        }

       

        [HttpDelete("{groupId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteGroup(int groupId)
        {

            var existingGroup= _groupRepository.GetGroup(groupId);

            if(existingGroup==null)
                return NotFound();
            if (!_groupRepository.DeleteGroup(existingGroup))
                return StatusCode(500);
            return NoContent();
        }
    }
}
