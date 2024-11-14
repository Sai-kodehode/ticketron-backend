namespace Ticketron.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<UnregUser> UnregUsers { get; set; } = new List<UnregUser>();
    }
}
