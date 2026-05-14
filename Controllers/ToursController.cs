using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.DTOs;
using Backend;

namespace Backend.Controllers;

[ApiController]
[Route("api/tours")]
public class ToursController : ControllerBase
{
    private readonly ITourService _tourService;

    public ToursController(ITourService tourService)
    {
        _tourService = tourService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tours = await _tourService.GetAllTours();
        return Ok(tours);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tour = await _tourService.GetTourById(id);
        if (tour == null) return NotFound();
        return Ok(tour);
    }

    [UserMod]
    [HttpPost("{id}/book")]
    public async Task<IActionResult> Book(int id)
    {
        var tour = await _tourService.GetTourById(id);
        if (tour == null) return NotFound();

        return Ok(new
        {
            Message = "Tour booked successfully",
            Tour = tour
        });
    }

    [AdminMod]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TourDto dto)
    {
        var created = await _tourService.CreateTour(dto);
        return Ok(created);
    }

    [AdminMod]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TourDto dto)
    {
        var updated = await _tourService.UpdateTour(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [AdminMod]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _tourService.DeleteTour(id);
        if (!result) return NotFound();
        return Ok();
    }
}
