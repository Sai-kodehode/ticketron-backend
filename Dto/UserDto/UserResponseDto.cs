namespace Ticketron.Dto.UserDto
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
    }
}
