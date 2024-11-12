namespace Ticketron.Models
{
    public class GroupMember
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public UnregUser? UnregUser { get; set; }
        public Group? Group { get; set; }
    }
}
