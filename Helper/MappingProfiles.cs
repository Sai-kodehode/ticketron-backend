using AutoMapper;
using Ticketron.Dto.BookingDto.BookingDto;
using Ticketron.Dto.GroupDto.GroupDto;
using Ticketron.Dto.GroupMemberDto;
using Ticketron.Dto.ParticipantDto;
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
            CreateMap<User, UserCreateDto>().ReverseMap();

            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<BookingCreateDto, Booking>();
            CreateMap<BookingUpdateDto, Booking>();

            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<TicketCreateDto, Ticket>().ReverseMap();
            CreateMap<TicketUpdateDto, Ticket>().ReverseMap();

            CreateMap<UnregUserCreateDto, UnregUser>();
            CreateMap<UnregUser, UnregUserResponseDto>();
            CreateMap<Participant, ParticipantResponseDto>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Booking.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User != null ? src.User.Id : (Guid?)null))
                .ForMember(dest => dest.UnregUserId, opt => opt.MapFrom(src => src.UnregUser != null ? src.UnregUser.Id : (Guid?)null))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Group != null ? src.Group.Id : (Guid?)null));
            CreateMap<ParticipantCreateDto, Participant>()
                .ForMember(dest => dest.Booking, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UnregUser, opt => opt.Ignore());
            CreateMap<Group, GroupResponseDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.GroupMemberIds, opt => opt.MapFrom(src => src.GroupMembers.Select(gm => gm.Id)));
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();

            CreateMap<GroupMemberDto, GroupMember>().ReverseMap();

        }
    }
}