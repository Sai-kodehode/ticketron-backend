using AutoMapper;
using Ticketron.Dto.BookingDto;
using Ticketron.Dto.BookingDto.BookingDto;
using Ticketron.Dto.GroupDto.GroupDto;
using Ticketron.Dto.TicketDto;
using Ticketron.Dto.UnregUserDto;
using Ticketron.Dto.UserDto;
using Ticketron.Models;

namespace Ticketron.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserResponseDto>();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BookingCreateDto, Booking>()
                .ForMember(dest => dest.Groups, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.UnregUsers, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Tickets, opt => opt.Ignore());
            CreateMap<Booking, BookingResponseDto>();
            CreateMap<BookingUpdateDto, Booking>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Booking, BookingSummaryResponseDto>();

            CreateMap<TicketCreateDto, Ticket>()
                .ForMember(dest => dest.AssignedUser, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedUnregUser, opt => opt.Ignore())
                .ForMember(dest => dest.Booking, opt => opt.Ignore())
                .ForMember(dest => dest.PurchasedBy, opt => opt.Ignore());
            CreateMap<Ticket, TicketResponseDto>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Booking.Id));
            CreateMap<TicketUpdateDto, Ticket>()
                .ForMember(dest => dest.AssignedUser, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedUnregUser, opt => opt.Ignore())
                .ForMember(dest => dest.PurchasedBy, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UnregUserCreateDto, UnregUser>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<UnregUser, UnregUserResponseDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Id));

            CreateMap<Group, GroupResponseDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Id));
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();
        }
    }
}