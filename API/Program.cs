using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
    
// Add the generic repo as a scoped service
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Allow spa
builder.Services.AddCors();

var app = builder.Build();

// Configure pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.MapControllers();

app.Run();
