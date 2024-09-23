namespace Ticketron.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Booking Booking { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<TicketParticipant> TicketParticipants { get; set; }



    }
}
