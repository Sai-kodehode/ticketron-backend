namespace Ticketron.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public required User User { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}