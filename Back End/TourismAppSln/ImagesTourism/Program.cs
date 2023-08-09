using ImagesTourism.Interfaces;
using ImagesTourism.Models;
using ImagesTourism.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ImageContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));
builder.Services.AddScoped<IRepo<int, ImageTourism>, TourImageRepo>();
builder.Services.AddScoped<ITourImageServices, TourImageService>();



builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AngularCORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseCors("AngularCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
