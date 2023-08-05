using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Services; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TourPackageContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));
builder.Services.AddScoped<IRepo<int,Package>,PackageRepo>();
builder.Services.AddScoped<IRepo<int,Itinerary>,ItineraryRepo>();
//builder.Services.AddScoped<IRepo<int,Hotel>,HotelRepo>(); 
builder.Services.AddScoped<IRepo<int, ContactDetails>, ContactDetailsRepo>();
builder.Services.AddScoped<IContactDetailsServices, ContactDetailsService>();
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
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")), // Use app.Environment.ContentRootPath
//    RequestPath = "/Images"
//});
app.UseAuthentication();
app.UseCors("AngularCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
