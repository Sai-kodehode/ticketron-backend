namespace Ticketron.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required User CreatedBy { get; set; }
        public ICollection<User> Users { get; set; } = [];
        public ICollection<UnregUser> UnregUsers { get; set; } = [];
    }
}