namespace Ticketron.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? BlobName { get; set; }
        public Participant? Participant { get; set; }
        public Guid BookingId { get; set; }
        public required Booking Booking { get; set; }
    }
}