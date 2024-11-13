namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingUpdateDto
    {
        public required Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}