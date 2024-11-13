namespace Ticketron.Dto.TicketDto
{
    public class TicketCreateDto
    {
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? imageUrl { get; set; }
        public Guid? ParticipantId { get; set; }
        public required Guid BookingId { get; set; }
    }
}
