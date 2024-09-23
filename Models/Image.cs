using System.Reflection.Metadata;

namespace Ticketron.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Imageurl { get; set; }
        public User Ticket { get; set; }
    }
}
