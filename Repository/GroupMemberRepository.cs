using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class GroupMemberRepository : IGroupMemberRepository
    {
        private readonly DataContext _context;

        public GroupMemberRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGroupMember(GroupMember groupMember)
        {
            _context.Add(groupMember);
            return Save();
        }

        public bool DeleteGroupMember(GroupMember groupMember)
        {
            _context.Remove(groupMember);
            return Save();
        }


        public GroupMember GetGroupMember(int groupmemberId)
        {
            return _context.GroupMembers.Include(gm => gm.User).Include(gm => gm.UnregUser).FirstOrDefault(gm => gm.Id == groupmemberId);

        }



        public ICollection<GroupMember> GetGroupMembers(int groupId)
        {
            return _context.GroupMembers.Include(gm=>gm.User).Include(gm=>gm.UnregUser).Where(x => x.Group.Id == groupId).ToList();
        }
  public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
        public bool GroupMemberExists(int groupMemeberId)
        {
            return _context.GroupMembers.Any(x => x.Id == groupMemeberId);
        }


    }
}
