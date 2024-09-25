namespace Ticketron.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<UnregUser>? UnregUsers { get; set; }
    }
}
