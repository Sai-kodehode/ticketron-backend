namespace Ticketron.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public Ticket Ticket { get; set; }
    }
}
