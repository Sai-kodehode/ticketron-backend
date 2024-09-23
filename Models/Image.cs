using System.Reflection.Metadata;

namespace Ticketron.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Imageurl { get; set; }
        public Ticket Ticket { get; set; }
    }
}
