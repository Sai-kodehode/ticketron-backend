using Ticketron.Dto.TicketDto;

namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingResponseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Guid>? ParticipantIds { get; set; }
        public IEnumerable<TicketResponseDto>? Tickets { get; set; }
    }
}