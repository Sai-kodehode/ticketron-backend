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
            CreateMap<ParticipantDto, Participant>();
            //CreateMap<Participant, ParticipantDto>()
            //    .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Booking.Id))
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User != null ? src.User.Id : (int?)null))
            //    .ForMember(dest => dest.UnregUserId, opt => opt.MapFrom(src => src.UnregUser != null ? src.UnregUser.Id : (int?)null));
        }
    }
}
