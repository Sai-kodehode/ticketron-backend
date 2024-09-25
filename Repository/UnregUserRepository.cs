using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class UnregUserRepository : IUnregUserRepository
    {
        private readonly DataContext _context;

        public UnregUserRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUnregUser(UnregUser unregUser)
        {
            _context.Add(unregUser);
            return Save();
        }

        public bool DeleteUnregUser(UnregUser unregUser)
        {
            _context.Remove(unregUser);
            return Save();
        }

        public UnregUser GetUnregUser(int unregUserId)
        {
            return _context.UnregUsers.Where(x => x.Id == unregUserId).FirstOrDefault();
        }

        public ICollection<UnregUser> GetUnregUsersByUserId(int userId)
        {
            return _context.UnregUsers.Where(x => x.User.Id == userId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UnregUserExists(int unregUserId)
        {
            return _context.UnregUsers.Any(x => x.Id == unregUserId);
        }
    }
}
