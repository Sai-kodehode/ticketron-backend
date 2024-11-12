namespace Ticketron.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required User User { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<Participant>? Participants { get; set; }
    }
}