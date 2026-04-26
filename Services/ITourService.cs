using Backend.DTOs;

namespace Backend.Services;

public interface ITourService
{
    List<TourDto> GetAllTours();
}