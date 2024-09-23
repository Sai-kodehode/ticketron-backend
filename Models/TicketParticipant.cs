namespace Ticketron.Models
{
    public class TicketParticipant
    {
        public int TicketId { get; set; }
        public int ParticipantId { get; set; }
        public Ticket Ticket { get; set; }
        public Participant Participant { get; set; }




    }
}
