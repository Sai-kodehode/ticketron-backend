namespace Ticketron.Models
{
    public class Groupmember
    {
        public int Id { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Unreguser> Unregusers { get; set; }

       
     
    }
}
