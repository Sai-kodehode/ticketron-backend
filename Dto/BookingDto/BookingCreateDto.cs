namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingCreateDto
    {
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
