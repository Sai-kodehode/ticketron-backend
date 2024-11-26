namespace Ticketron.Dto.GroupDto.GroupDto
{
    public class GroupUpdateDto
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Guid>? UserIds { get; set; }
        public ICollection<Guid>? UnregUserIds { get; set; }
    }
}
