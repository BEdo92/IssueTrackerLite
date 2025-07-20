using AutoMapper;
using IssueTrackerLite.Data;
using IssueTrackerLite.DTOs;

namespace IssueTrackerLite.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Registration
        CreateMap<RegisterDto, AppUser>();

        // Tickets
        CreateMap<TicketDto, Ticket>()
            .ForMember(dest => dest.DateAdded, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => "open"))
            .ForMember(dest => dest.User, opt => opt.Ignore()); // Set User in controller after mapping
    }
}
