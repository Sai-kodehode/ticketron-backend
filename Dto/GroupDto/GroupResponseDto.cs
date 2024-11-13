namespace Ticketron.Dto.GroupDto.GroupDto
{
    public class GroupResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid CreatedBy { get; set; }
        public required ICollection<Guid> GroupMemberIds { get; set; }

    }
}
