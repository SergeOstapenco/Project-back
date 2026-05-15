namespace Backend.DTOs;

public class TourDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Img { get; set; } = string.Empty;
}