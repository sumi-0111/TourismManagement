using Microsoft.EntityFrameworkCore;
using TravellerFeedBackAPI.Interface;
using TravellerFeedBackAPI.Models;
using TravellerFeedBackAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FeedBackContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
builder.Services.AddScoped<IRepo<int,UserFeedBack>,FeedBackRepo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
