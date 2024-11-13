namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingUpdateDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}