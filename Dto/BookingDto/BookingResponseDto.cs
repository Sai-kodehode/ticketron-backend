using Ticketron.Dto.TicketDto;

namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingResponseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public ICollection<Guid>? ParticipantIds { get; set; }
        public ICollection<TicketResponseDto>? Tickets { get; set; }
    }
}