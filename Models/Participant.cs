namespace Ticketron.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group Group { get; set; }
        public ICollection<BookingParticipant> BookingParticipants { get; set; }

        public ICollection<TicketParticipant> TicketParticipants { get; set; }

    }
}
