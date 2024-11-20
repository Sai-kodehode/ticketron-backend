namespace Ticketron.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public required User CreatedBy { get; set; }
        public required Guid CreatedById { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<User> Users { get; set; } = [];
        public ICollection<UnregUser> UnregUsers { get; set; } = [];
        public ICollection<Group> Groups { get; set; } = [];
        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}