using AutoMapper;
using Ticketron.Dto;
using Ticketron.Models;

namespace Ticketron.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<UnregUser, UnregUserDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            //CreateMap<GroupMember, GroupMemberDto>().ReverseMap();
        }
    }
}
