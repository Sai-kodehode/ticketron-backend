using Ticketron.Dto.UnregUserDto;
using Ticketron.Dto.UserDto;

namespace Ticketron.Dto.TicketDto
{
    public class TicketResponseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Category { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? PurchaseDate { get; set; }
        public int? Price { get; set; }
        public string? ImageUrl { get; set; }
        public UserResponseDto? PurchasedBy { get; set; }
        public UserResponseDto? AssignedUser { get; set; }
        public UnregUserResponseDto? AssignedUnregUser { get; set; }
        public Guid BookingId { get; set; }
    }
}