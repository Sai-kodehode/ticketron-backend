namespace Ticketron.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public ICollection<UnregUser>? UnregUsers { get; set; }
    }
}
