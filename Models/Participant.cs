namespace Ticketron.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public int AddedBy { get; set; }
        public required Booking Booking { get; set; }
        public User? User { get; set; }
        public UnregUser? UnregUser { get; set; }
        public Group? Group { get; set; }
        public bool IsUser { get; set; }
    }
}
