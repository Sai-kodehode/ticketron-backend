namespace Ticketron.Dto.ParticipantDto
{
    public class ParticipantResponseDto
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid BookingId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UnregUserId { get; set; }
        public Guid? GroupId { get; set; }
    }
}
