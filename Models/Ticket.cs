namespace Ticketron.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? BlobName { get; set; }
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}