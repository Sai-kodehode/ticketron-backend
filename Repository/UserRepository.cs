using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(x => x.Id == userId).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(x => x.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser == null)
                return false;

            _context.Entry(existingUser).State = EntityState.Detached;
            _context.Update(user);
            return Save();
        }
 public bool UserExists(int userId)
        {
            return _context.Users.Any(x => x.Id == userId);
        }
    }
}
