using AutoMapper;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Services;

public class TourService : ITourService
{
    private readonly IMapper _mapper;

    public TourService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<TourDto> GetAllTours()
    {
        var tours = new List<Tour>
        {
            new Tour { Id = 1, Title = "Бали", Price = 1200, Category = "Пляж", Img = "https://picsum.photos/seed/51/800/600" },
            new Tour { Id = 2, Title = "Альпы", Price = 1500, Category = "Горы", Img = "https://picsum.photos/seed/52/800/600" },
            new Tour { Id = 3, Title = "Токио", Price = 2100, Category = "Город", Img = "https://picsum.photos/seed/53/800/600" }
        };

        return _mapper.Map<List<TourDto>>(tours);
    }
}