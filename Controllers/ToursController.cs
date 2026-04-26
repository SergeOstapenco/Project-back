using Microsoft.AspNetCore.Mvc;
using Backend.Services;

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
    public IActionResult Get()
    {
        
        var tours = _tourService.GetAllTours();
        return Ok(tours);
    }
}