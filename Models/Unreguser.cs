namespace Ticketron.Models
{
    public class UnregUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
