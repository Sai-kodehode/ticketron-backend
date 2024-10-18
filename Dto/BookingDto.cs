//namespace Ticketron.Dto
//{
//    public class BookingDto
//    {

//        public int Id { get; set; }
//        public string Title { get; set; }
//        public DateTime StartDate { get; set; }
//        public DateTime EndDate { get; set; }
//    }
//}

namespace Ticketron.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }  // Optional, used for updates
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // This property must be present to capture the user associated with the booking
        public int UserId { get; set; }
    }
}
