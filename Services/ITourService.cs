using Backend.DTOs;

namespace Backend.Services;

public interface ITourService
{
    Task<List<TourDto>> GetAllTours();
    Task<TourDto> GetTourById(int id);
    Task<TourDto> CreateTour(TourDto dto);
    Task<TourDto> UpdateTour(int id, TourDto dto);
    Task<bool> DeleteTour(int id);
}