using Ticketron.Dto.GroupDto.GroupDto;
using Ticketron.Dto.TicketDto;
using Ticketron.Dto.UnregUserDto;
using Ticketron.Dto.UserDto;

namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingResponseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public required UserResponseDto CreatedBy { get; set; }
        public ICollection<UserResponseDto> Users { get; set; } = [];
        public ICollection<UnregUserResponseDto> UnregUsers { get; set; } = [];
        public ICollection<GroupResponseDto> Groups { get; set; } = [];
        public ICollection<TicketResponseDto> Tickets { get; set; } = [];
    }
}