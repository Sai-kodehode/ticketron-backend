namespace Ticketron.Dto.GroupDto.GroupDto
{
    public class GroupCreateDto
    {
        public required string Name { get; set; }
        public ICollection<Guid> UserIds { get; set; } = [];
        public ICollection<Guid> UnregUserIds { get; set; } = [];
    }
}
