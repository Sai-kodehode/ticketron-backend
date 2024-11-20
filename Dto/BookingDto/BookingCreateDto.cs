namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingCreateDto
    {
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Guid> UserIds { get; set; } = [];
        public ICollection<Guid> UnregUserIds { get; set; } = [];
        public ICollection<Guid> GroupIds { get; set; } = [];
    }
}
