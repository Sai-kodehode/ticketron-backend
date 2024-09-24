namespace Ticketron.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User? User { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
