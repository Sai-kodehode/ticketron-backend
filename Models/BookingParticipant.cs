namespace Ticketron.Models
{
    public class BookingParticipant
    {
        public int BookingId {  get; set; }
        public int ParticipantId { get; set; }

        public Booking Booking { get; set; }
        public Participant Participant { get; set; }

    }
}
