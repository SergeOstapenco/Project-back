using Backend.Mappings;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Service.AddDbContext<AppDbContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnection.String("DefaultConnection")))

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseCors("AllowAll");
app.MapControllers();

app.Run();