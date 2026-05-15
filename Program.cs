using Backend.Mappings;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Services;
using Backend;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5001");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITourService, TourService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<RoleHeaderOperationFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
});

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();

public class RoleHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAdminMod = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AdminModAttribute>()
            .Any()
            || context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .OfType<AdminModAttribute>()
                .Any() == true;

        var hasUserMod = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<UserModAttribute>()
            .Any()
            || context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .OfType<UserModAttribute>()
                .Any() == true;

        if (!hasAdminMod && !hasUserMod)
        {
            return;
        }

        operation.Parameters ??= new List<IOpenApiParameter>();
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Role",
            In = ParameterLocation.Header,
            Required = true,
            Description = hasAdminMod
                ? "Введите Admin для доступа к этому действию."
                : "Введите User или Admin для доступа к этому действию."
        });
    }
}
