using AutoMapper;
using Database;
using Map;
using Microsoft.OpenApi.Models;
using shiftlogger.Interface;
using shiftlogger.Repositories;
using shiftlogger.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
IMapper mapper = Map.Mapping.InitializeAutomapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddDbContext<MyContext>();
builder.Services.AddScoped(typeof(IBaseRepository), typeof(LoggerRepository));
builder.Services.AddScoped(typeof(IBaseService), typeof(LoggerService));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Logger API",
        Description = "An ASP.NET Core Web API for Logging Activities ",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
