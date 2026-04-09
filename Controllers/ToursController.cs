using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/tours")]
public class ToursController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var tours = new[]
        {
            new { id = 1, title = "Бали", price = 1200, category = "Пляж", img = "https://picsum.photos/seed/51/800/600" },
            new { id = 2, title = "Альпы", price = 1500, category = "Горы", img = "https://picsum.photos/seed/52/800/600" },
            new { id = 3, title = "Токио", price = 2100, category = "Город", img = "https://picsum.photos/seed/53/800/600" },
            new { id = 4, title = "Исландия", price = 1800, category = "Природа", img = "https://picsum.photos/seed/54/800/600" },
            new { id = 5, title = "Рим", price = 900, category = "Город", img = "https://picsum.photos/seed/55/800/600" },
            new { id = 6, title = "Париж", price = 1100, category = "Город", img = "https://picsum.photos/seed/56/800/600" },
            new { id = 7, title = "Норвегия", price = 1700, category = "Природа", img = "https://picsum.photos/seed/57/800/600" },
            new { id = 8, title = "Мальдивы", price = 2500, category = "Пляж", img = "https://picsum.photos/seed/58/800/600" },
            new { id = 9, title = "Пхукет", price = 1000, category = "Пляж", img = "https://picsum.photos/seed/59/800/600" },
            new { id = 10, title = "Доломиты", price = 1400, category = "Горы", img = "https://picsum.photos/seed/60/800/600" }
        };
        return Ok(tours);
    }
}