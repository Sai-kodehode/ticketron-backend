using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IGroupRepository
    {
        ICollection<Group> GetGroups(int userId);
        Group GetGroup(int groupId);
        bool GroupExists(int groupId);
        bool CreateGroup(Group group);
        bool UpdateGroup(Group group);
        bool DeleteGroup(Group group);
        bool Save();





    }
}
