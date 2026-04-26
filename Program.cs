using Backend.Mappings;

var builder = WebApplication.CreateBuilder(args);

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