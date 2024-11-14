namespace Ticketron.Dto.BookingDto
{
    public class BookingSummaryResponseDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
