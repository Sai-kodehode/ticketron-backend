namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingUpdateDto
    {
        public required Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? CreatedBy { get; set; }
        public ICollection<Guid>? UserIds { get; set; }
        public ICollection<Guid>? UnregUserIds { get; set; }
        public ICollection<Guid>? GroupIds { get; set; }

    }
}