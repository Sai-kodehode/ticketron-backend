namespace Ticketron.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int? Price { get; set; }
        public DateTimeOffset? PurchaseDate { get; set; }
        public required string Category { get; set; }
        public string? ImageUrl { get; set; }
        public Participant? Participant { get; set; }
        public required Booking Booking { get; set; }
    }
}