namespace Ticketron.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string? AddedBy { get; set; }
        public Booking? Booking { get; set; }
        public User? User { get; set; }
        public UnregUser? UnregUser { get; set; }
        public bool? IsUser { get; set; }
    }
}
