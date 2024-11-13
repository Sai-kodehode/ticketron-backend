using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto.UserDto;
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
        private readonly IUserContextService _userContextService;

        public UserController(IUserRepository userRepository, IMapper imapper, IUserContextService userContextService)
        {
            _userRepository = userRepository;
            _mapper = imapper;
            _userContextService = userContextService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserCreateDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<List<UserCreateDto>>(await _userRepository.GetUsersAsync());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(UserCreateDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound();

            var userMap = _mapper.Map<UserCreateDto>(user);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(userMap);
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto newUser)
        {
            if (newUser == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var userExisting = await _userRepository.GetUserByEmailAsync(newUser.Email);

            if (userExisting != null)
                return Conflict();

            var userMap = _mapper.Map<User>(newUser);

            Guid currentUserId;
            try
            {
                currentUserId = _userContextService.GetUserObjectId();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

            userMap.Id = currentUserId;

            if (!await _userRepository.CreateUserAsync(userMap))
                return Problem();

            return Created();
        }

        //[HttpPut("{userId}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(409)]
        //[ProducesResponseType(500)]
        //public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto updatedUser)
        //{
        //    Guid userId;
        //    try
        //    {
        //        userId = _userContextService.GetUserObjectId();
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        return Unauthorized(ex.Message);
        //    }

        //    if (updatedUser == null)
        //        return BadRequest();

        //    if (await !_userRepository.UserExistsAsync(objectId))
        //        return NotFound();

        //    var existingUser = _userRepository.GetUser(objectId);

        //    if (existingUser.Email != updatedUser.Email)
        //        return Conflict();


        //    var userMap = _mapper.Map<User>(updatedUser);

        //    userMap.Id = userId;

        //    if (!_userRepository.UpdateUser(userMap))
        //        return StatusCode(500);

        //    return NoContent();
        //}

        //[HttpDelete("{userId}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]
        //public IActionResult DeleteUser(int userId)
        //{
        //    var objectIdString = User.FindFirst("oid")?.Value;

        //    if (!Guid.TryParse(objectIdString, out var objectId))
        //    {
        //        return Unauthorized("Can't convert string objectId to guid");
        //    }

        //    var existingUser = _userRepository.GetUser(objectId);

        //    if (existingUser == null)
        //        return NotFound();

        //    if (!_userRepository.DeleteUser(existingUser))
        //        return StatusCode(500);

        //    return NoContent();
        //}

    }
}
