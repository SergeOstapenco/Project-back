using AutoMapper;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tour, TourDto>().ReverseMap();
    }
}