namespace Ticketron.Dto.ParticipantDto
{
    public class ParticipantCreateDto
    {
        public Guid BookingId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UnregUserId { get; set; }
    }
}