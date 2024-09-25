namespace Ticketron.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
<<<<<<< HEAD
        public Participant? Participant { get; set; }
        public ICollection<Image>? Images { get; set; }
        public Booking? Booking { get; set; }


=======
        public string? ImageUrl { get; set; }
        public Participant Participant { get; set; }
        public Booking Booking { get; set; }
>>>>>>> 8fe77fad60a1512e859830a4ec15239982a6663b
    }
}