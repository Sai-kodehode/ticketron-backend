namespace Ticketron.Dto.GroupMemberDto
{
    public class GroupMemberDto
    {
        public required Guid GroupId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UnregUserId { get; set; }
        public required string Name { get; set; }
    }
}