namespace Ticketron.Dto.UnregUserDto
{
    public class UnregUserResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
