namespace Ticketron.Dto.UserDto
{
    public class UserCreateDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public Guid AzureObjectId { get; set; }

    }
}
