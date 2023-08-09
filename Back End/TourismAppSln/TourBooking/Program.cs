using Microsoft.EntityFrameworkCore;
using TourBooking.Interfaces;
using TourBooking.Models;
using TourBooking.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookingContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));
builder.Services.AddScoped<IManageBooking, ManageBookingService>();
builder.Services.AddScoped<IRepo<int, Booking>, BookingRepo>();
builder.Services.AddScoped<ICalculateService, CalculateService>();

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
