var builder = WebApplication.CreateBuilder(args);

// Разрешаем фронтенду доступ (CORS)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowAll");
app.MapControllers();

app.Run();
