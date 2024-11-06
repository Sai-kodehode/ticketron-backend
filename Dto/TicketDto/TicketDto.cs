namespace Ticketron.Dto.TicketDto
{
    public class TicketDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public int ParticipantId { get; set; }
        public int BookingId { get; set; }

    }

}