using Microsoft.AspNetCore.Mvc;
using Ticketron.Dto;
using Ticketron.Interfaces;
using Ticketron.Models;
using AutoMapper;

namespace Ticketron.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsersController:Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper imapper)
        {
            _userRepository = userRepository;
            _mapper = imapper;

        }

        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
            public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
        
            if(!ModelState.IsValid) 
                return BadRequest ();
            return Ok(users);
        }

        [HttpGet("{id}")]

        public IActionResult GetUser(int id)

        {
            if (!_userRepository.UserExists(id))
                return NotFound ();
                var user = _mapper.Map<UserDto>(_userRepository.GetUser(id));
            if(!ModelState.IsValid)
                return BadRequest ();
            return Ok(user);

        }

        [HttpPost]

        public IActionResult CreateUser([FromBody] UserDto newUser)
        {

            if (CreateUser == null) 
                return BadRequest();

            var userExisting = _userRepository.GetUsers().FirstOrDefault(c =>  c.Email == newUser.Email);

            if (userExisting != null)
                return Conflict();

            if (!ModelState.IsValid) 
                return BadRequest();

            var userMap = _mapper.Map<User>(newUser);

            if (!_userRepository.CreateUser(userMap))
            {
               return StatusCode(500);
            }
            return StatusCode(201);
        }

        [HttpPut]

        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser==null)
                return BadRequest();
            if (userId!=updatedUser.Id)
                return BadRequest();
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var existingUser = _userRepository.GetUser(userId);

            if (existingUser.Email != updatedUser.Email)
            {
                return Conflict();
            }


            var userMap= _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
                return StatusCode (500);

            return NoContent();
           


        }

    }
}
