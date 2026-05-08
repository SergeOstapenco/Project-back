using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/tours")]
public class ToursController : ControllerBase
{
    private readonly AppDbContext _context;

    public ToursController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tours = await _context.Tours.ToListAsync();
        return Ok(tours);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tour = await _context.Tours.FindAsync(id);

        if (tour == null)
            return NotFound();

        return Ok(tour);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tour tour)
    {
        _context.Tours.Add(tour);
        await _context.SaveChangesAsync();

        return Ok(tour);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tour updatedTour)
    {
        var tour = await _context.Tours.FindAsync(id);

        if (tour == null)
            return NotFound();

        tour.Title = updatedTour.Title;
        tour.Price = updatedTour.Price;
        tour.Category = updatedTour.Category;
        tour.Img = updatedTour.Img;

        await _context.SaveChangesAsync();

        return Ok(tour);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tour = await _context.Tours.FindAsync(id);

        if (tour == null)
            return NotFound();

        _context.Tours.Remove(tour);
        await _context.SaveChangesAsync();

        return Ok();
    }
}