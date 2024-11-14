namespace Ticketron.Dto.TicketDto
{
    public class TicketCreateDto
    {
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string? imageUrl { get; set; }
        public Guid? ParticipantId { get; set; }
        public required Guid BookingId { get; set; }
    }
}
