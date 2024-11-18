using Ticketron.Dto.UnregUserDto;
using Ticketron.Dto.UserDto;

namespace Ticketron.Dto.GroupDto.GroupDto
{
    public class GroupResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid CreatedBy { get; set; }
        public ICollection<UserResponseDto> Users { get; set; } = [];
        public ICollection<UnregUserResponseDto> UnregUsers { get; set; } = [];
    }
}