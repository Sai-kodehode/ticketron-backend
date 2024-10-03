namespace Ticketron.Models
{
    public class GroupMember
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? UnregUserId { get; set; }
        public UnregUser UnregUser { get; set; }
        public Group Group { get; set; }
        public bool IsUser { get; set; }
    }
}
