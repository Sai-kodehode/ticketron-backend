namespace Ticketron.Dto.TicketDto
{
    public class TicketUpdateDto
    {
        public required Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? ParticipantId { get; set; }

    }

}
