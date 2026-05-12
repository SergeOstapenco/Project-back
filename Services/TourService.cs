using AutoMapper;
using Backend.Models;
using Backend.DTOs;
using Backend.Data; 
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class TourService : ITourService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context; 

    public TourService(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

   
    public List<TourDto> GetAllTours() 
    {
        var tours = _context.Tours.ToList();
        return _mapper.Map<List<TourDto>>(tours);
    }

    public TourDto? GetTourById(int id)
    {
        var tour = _context.Tours.FirstOrDefault(t => t.Id == id);
        return _mapper.Map<TourDto>(tour);
    }

    public TourDto CreateTour(TourDto tourDto)
    {
        var tour = _mapper.Map<Tour>(tourDto);
        _context.Tours.Add(tour);
        _context.SaveChanges(); 
        return _mapper.Map<TourDto>(tour);
    }

    public bool UpdateTour(int id, TourDto tourDto)
    {
        var existingTour = _context.Tours.FirstOrDefault(t => t.Id == id);
        if (existingTour == null) return false;

        _mapper.Map(tourDto, existingTour);
        _context.SaveChanges(); 
        return true;
    }

    public bool DeleteTour(int id)
    {
        var tour = _context.Tours.FirstOrDefault(t => t.Id == id);
        if (tour == null) return false;

        _context.Tours.Remove(tour);
        _context.SaveChanges(); 
        return true;
    }
}