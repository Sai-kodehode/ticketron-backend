using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;
using Microsoft.EntityFrameworkCore;

namespace Ticketron.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGroup(Group group)
        {
           _context.Add(group);
            return Save();
        }

        public bool DeleteGroup(Group group)
        {
            _context.Remove(group);
            return Save();
        }

        public Group GetGroup(int groupId)
        {
            return _context.Groups.Where(x => x.Id == groupId).FirstOrDefault();
        }


        public ICollection<Group> GetGroups(int userId)
        {
            return _context.Groups.Where(x => x.User.Id == userId).ToList();
        }

        public bool GroupExists(int groupId)
        {
            return _context.Groups.Any(x => x.Id == groupId);
        }

        public bool Save()
        {
            var Saved=_context.SaveChanges();
            return Saved > 0;
        }

        public bool UpdateGroup(Group group)
        {
            var existingGroup=_context.Groups.Find(group.Id);
            if (existingGroup == null) 
                return false;
            _context.Entry(existingGroup).State=EntityState.Detached;
            _context.Update(group);
            return Save();

        }
    }
}
