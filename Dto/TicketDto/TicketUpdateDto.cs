namespace Ticketron.Dto.TicketDto
{
    public class TicketUpdateDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; }

        public int ParticipantId { get; set; }
        public int BookingId { get; set; }
    }

}
