//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Ticketron.Dto;
//using Ticketron.Interfaces;
//using Ticketron.Repository;

//namespace Ticketron.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]

//    public class GroupMemberController : Controller
//    {
//        private readonly IGroupMemberRepository _groupMemberRepository;
//        private readonly IMapper _mapper;
//        private readonly IGroupRepository _groupRepository;

//        public GroupMemberController(IGroupMemberRepository groupMemberRepository, IMapper mapper, IGroupRepository groupRepository )
//        {
//            _groupMemberRepository = groupMemberRepository;
//            _mapper = mapper;
//            _groupRepository = groupRepository;
//        }

//        [HttpGet("{groupMemberId}")]
//        public IActionResult GetTicket(int groupMemberId)
//        {
//            if (!_groupMemberRepository.GroupMemberExists(groupMemberId))
//                return NotFound();
//            var groupMember = _mapper.Map<GroupMemberDto>(_groupMemberRepository.GetGroupMember(groupMemberId));

//            if (!ModelState.IsValid)

//                return BadRequest();
//            return Ok(groupMember);

//        }





//    }
//}
