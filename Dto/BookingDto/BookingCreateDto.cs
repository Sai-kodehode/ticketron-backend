namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingCreateDto
    {
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
