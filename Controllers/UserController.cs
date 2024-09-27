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
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(user);
        }

        [HttpPost]
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
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest();

            if (!_userRepository.UserExists(userId))
                return NotFound();

            var existingUser = _userRepository.GetUser(userId);

            if (existingUser.Email != updatedUser.Email)
                return Conflict();


            var userMap = _mapper.Map<User>(updatedUser);

            userMap.Id = userId;

            if (!_userRepository.UpdateUser(userMap))
                return StatusCode(500);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var existingUser = _userRepository.GetUser(userId);

            if (existingUser == null)
                return NotFound();

            if (!_userRepository.DeleteUser(existingUser))
                return StatusCode(500);

            return NoContent();
        }

    }
}
