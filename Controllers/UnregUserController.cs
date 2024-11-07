//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Ticketron.Dto;
//using Ticketron.Interfaces;
//using Ticketron.Models;

//namespace Ticketron.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UnregUserController : ControllerBase
//    {
//        private readonly IUnregUserRepository _unregUserRepository;
//        private readonly IUserRepository _userRepository;
//        private readonly IMapper _mapper;

//        public UnregUserController(IUnregUserRepository unregUserRepository, IUserRepository userRepository, IMapper imapper)
//        {
//            _unregUserRepository = unregUserRepository;
//            _userRepository = userRepository;
//            _mapper = imapper;
//        }

//        [HttpGet("{unregUserId}")]
//        [ProducesResponseType(200, Type = typeof(UnregUser))]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(404)]

//        public IActionResult GetUnregUser(int unregUserId)
//        {
//            if (!_unregUserRepository.UnregUserExists(unregUserId))
//                return NotFound();

//            var unregUser = _mapper.Map<UnregUserDto>(_unregUserRepository.GetUnregUser(unregUserId));

//            if (!ModelState.IsValid)
//                return BadRequest();

//            return Ok(unregUser);
//        }

//        [HttpGet("user/{userId}")]
//        [ProducesResponseType(200, Type = typeof(IEnumerable<UnregUser>))]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(404)]
//        public IActionResult GetUnregUsersByUserId(int userId)
//        {
//            if (!_userRepository.UserExists(userId))
//                return NotFound();

//            var unregUsers = _mapper.Map<List<UnregUserDto>>(_unregUserRepository.GetUnregUsersByUserId(userId));

//            if (!ModelState.IsValid)
//                return BadRequest();

//            return Ok(unregUsers);

//        }

//        [HttpPost("{userId}")]
//        [ProducesResponseType(201)]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]

//        public IActionResult CreateUnregUser(int userId, [FromBody] UnregUserDto newUnregUser)
//        {
//            if (!_userRepository.UserExists(userId))
//                return NotFound();

//            if (newUnregUser == null)
//                return BadRequest();

//            var unregUserMap = _mapper.Map<UnregUser>(newUnregUser);

//            unregUserMap.User = _userRepository.GetUser(userId);

//            if (!ModelState.IsValid)
//                return BadRequest();

//            if (!_unregUserRepository.CreateUnregUser(unregUserMap))
//                return StatusCode(500);

//            return StatusCode(201);
//        }

//        [HttpDelete("{unregUserId}")]
//        [ProducesResponseType(204)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public IActionResult DeleteUnregUser(int unregUserId)
//        {
//            var existingUnregUser = _unregUserRepository.GetUnregUser(unregUserId);

//            if (existingUnregUser == null)
//                return NotFound();

//            if (!_unregUserRepository.DeleteUnregUser(existingUnregUser))
//                return StatusCode(500);

//            return NoContent();
//        }

//    }
//}
