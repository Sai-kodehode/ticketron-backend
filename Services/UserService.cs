using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUserIfNotExistAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser == null)
            {
                await _userRepository.CreateUserAsync(user);
                return user;
            }

            return existingUser;
        }
    }
}
