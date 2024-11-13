namespace Ticketron.Models
{
    public class Participant
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public required Booking Booking { get; set; }
        public User? User { get; set; }
        public UnregUser? UnregUser { get; set; }
        public Group? Group { get; set; }
        public bool IsUser { get; set; }
    }
}