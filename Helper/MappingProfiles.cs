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
            CreateMap<UnregUser, UnregUserDto>().ReverseMap();
        }
    }
}
