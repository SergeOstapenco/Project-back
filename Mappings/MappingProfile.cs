using AutoMapper;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tour, TourDto>();
        CreateMap<TourDto, Tour>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
