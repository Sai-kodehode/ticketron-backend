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
            if (groupMember.User != null)
            {
                groupMember.UserId = groupMember.User.Id;
                groupMember.User = null;
            }

            if (groupMember.UnregUser != null)
            {
                groupMember.UnregUserId = groupMember.UnregUser.Id; 
                groupMember.UnregUser = null; 
            }

            _context.GroupMembers.Add(groupMember);

            return Save();
        }

        public bool DeleteGroupMember(GroupMember groupMember)
        {
            if (groupMember.User != null)
            {
                groupMember.User = null;
            }

            if (groupMember.UnregUser != null)
            {
                groupMember.UnregUser = null;
            }

            _context.GroupMembers.Remove(groupMember);
            return Save();
        }

        public GroupMember GetGroupMember(int groupmemberId)
        {
            var groupMember = _context.GroupMembers
                .Include(gm => gm.User)        
                .Include(gm => gm.UnregUser)    
                .FirstOrDefault(gm => gm.Id == groupmemberId);

            if (groupMember != null)
            {
         
                if (groupMember.User != null)
                {
                    var userId = groupMember.User.Id;
                    groupMember.User = new User { Id = userId };
                }

          
                if (groupMember.UnregUser != null)
                {
                    var unregUserId = groupMember.UnregUser.Id;
                    groupMember.UnregUser = new UnregUser { Id = unregUserId };
                }
            }

            return groupMember;
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
