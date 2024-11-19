namespace Ticketron.Dto.GroupDto.GroupDto
{
    public class GroupUpdateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Guid> UserIds { get; set; } = [];
        public ICollection<Guid> UnregUserIds { get; set; } = [];
    }
}
