using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper imapper)
        {
            _userRepository = userRepository;
            _mapper = imapper;
        }

        [HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        //[ProducesResponseType(400)]

        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(users);
        }

        [HttpGet("{azureObjectId}")]
        //[ProducesResponseType(200, Type = typeof(User))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]

        public IActionResult GetUser(Guid azureObjectId)
        {
            if (!_userRepository.UserExists(azureObjectId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(azureObjectId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(user);
        }

        [HttpPost]

        //[ProducesResponseType(201)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(409)]
        //[ProducesResponseType(500)]

        public IActionResult CreateUser([FromBody] UserDto newUser)
        {


            if (newUser == null)
                return BadRequest();

            var userExisting = _userRepository.GetUsers().FirstOrDefault(c => c.Email == newUser.Email);

            if (userExisting != null)
                return Conflict();

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(newUser);

            if (!_userRepository.CreateUser(userMap))
                return StatusCode(500);

            return StatusCode(201);
        }

        [HttpPut("{userId}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(409)]
        //[ProducesResponseType(500)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            var objectIdString = User.FindFirst("oid")?.Value;

            if (!Guid.TryParse(objectIdString, out var objectId))
            {
                return Unauthorized("Can't convert string objectId to guid");
            }

            if (updatedUser == null)
                return BadRequest();

            if (!_userRepository.UserExists(objectId))
                return NotFound();

            var existingUser = _userRepository.GetUser(objectId);

            if (existingUser.Email != updatedUser.Email)
                return Conflict();


            var userMap = _mapper.Map<User>(updatedUser);

            userMap.Id = userId;

            if (!_userRepository.UpdateUser(userMap))
                return StatusCode(500);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]
        public IActionResult DeleteUser(int userId)
        {
            var objectIdString = User.FindFirst("oid")?.Value;

            if (!Guid.TryParse(objectIdString, out var objectId))
            {
                return Unauthorized("Can't convert string objectId to guid");
            }

            var existingUser = _userRepository.GetUser(objectId);

            if (existingUser == null)
                return NotFound();

            if (!_userRepository.DeleteUser(existingUser))
                return StatusCode(500);

            return NoContent();
        }

    }
}
