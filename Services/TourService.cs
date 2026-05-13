using Backend.Data;
using Backend.Models;
using Backend.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class TourService : ITourService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TourService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TourDto>> GetAllTours()
    {
        var tours = await _context.Tours.ToListAsync();
        return _mapper.Map<List<TourDto>>(tours);
    }

    public async Task<TourDto> GetTourById(int id)
    {
        var tour = await _context.Tours.FindAsync(id);
        return _mapper.Map<TourDto>(tour);
    }

    public async Task<TourDto> CreateTour(TourDto dto)
    {
        var tour = _mapper.Map<Tour>(dto);
        _context.Tours.Add(tour);
        await _context.SaveChangesAsync();
        return _mapper.Map<TourDto>(tour);
    }

    public async Task<TourDto> UpdateTour(int id, TourDto dto)
    {
        var tour = await _context.Tours.FindAsync(id);
        if (tour == null) return null;

        _mapper.Map(dto, tour);
        await _context.SaveChangesAsync();
        return _mapper.Map<TourDto>(tour);
    }

    public async Task<bool> DeleteTour(int id)
    {
        var tour = await _context.Tours.FindAsync(id);
        if (tour == null) return false;

        _context.Tours.Remove(tour);
        await _context.SaveChangesAsync();
        return true;
    }
}