namespace Ticketron.Dto.BookingDto.BookingDto
{
    public class BookingCreateDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
    }
}
