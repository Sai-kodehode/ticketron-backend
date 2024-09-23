using System.Collections;

namespace Ticketron.Models
{
    public class Unreguser
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
        

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
