namespace Ticketron.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string AddedBy { get; set; }

        public Booking Booking { get; set; }

        public int UserId {  get; set; }




   

    }
}
