namespace Ticketron.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? ImageUrl { get; set; }
        public Participant? Participant { get; set; }
        public Booking Booking { get; set; }

    }
}