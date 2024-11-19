namespace Ticketron.Models
{
    public class UnregUser
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required User CreatedBy { get; set; }
        public ICollection<Group> Groups { get; set; } = [];
    }
}