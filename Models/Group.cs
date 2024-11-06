namespace Ticketron.Models
{
    public class Group
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required User User { get; set; }
        public ICollection<GroupMember>? GroupMembers { get; set; }
    }
}