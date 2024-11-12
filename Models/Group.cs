namespace Ticketron.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required User User { get; set; }
        public required ICollection<GroupMember> GroupMembers { get; set; }
    }
}