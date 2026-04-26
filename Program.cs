using Backend.Mappings;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ITourService, TourService>();

var app = builder.Build();

app.UseCors("AllowAll");
app.MapControllers();

app.Run();