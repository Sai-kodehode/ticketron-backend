namespace Ticketron.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Category { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? PurchaseDate { get; set; }
        public int? Price { get; set; }
        public string? ImageUrl { get; set; }
        public User? PurchasedBy { get; set; }
        public required User AssignedUser { get; set; }
        public required Booking Booking { get; set; }
    }
}