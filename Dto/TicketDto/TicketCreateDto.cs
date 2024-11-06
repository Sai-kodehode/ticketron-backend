namespace Ticketron.Dto.TicketDto
{
    public class TicketCreateDto
    {
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IFormFile? Image { get; set; }
        public int ParticipantId { get; set; }
        public int BookingId { get; set; }
    }

}
