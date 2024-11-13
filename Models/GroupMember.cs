namespace Ticketron.Models
{
    public class GroupMember
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public UnregUser? UnregUser { get; set; }
        public required Group Group { get; set; }
    }
}
