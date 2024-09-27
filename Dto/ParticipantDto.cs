namespace Ticketron.Dto
{
    public class ParticipantDto
    {
        public int AddedBy { get; set; }
        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public int? UnregUserId { get; set; }
        public bool IsUser { get; set; }
    }
}
