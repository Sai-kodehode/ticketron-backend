using Ticketron.Dto.ParticipantDto;

namespace Ticketron.Dto.TicketDto
{
    public class TicketResponseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required ParticipantResponseDto Participant { get; set; }
        public Guid BookingId { get; set; }
        public string? imageUrl { get; set; }

    }

}