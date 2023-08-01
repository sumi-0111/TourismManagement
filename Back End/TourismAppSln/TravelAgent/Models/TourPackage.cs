using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models
{
    public class TourPackage
    {
        [Key] 
        public int TourId { get; set; }  

        [ForeignKey("TravelAgent")]
        public int TravelAgentId { get; set; }
        public string? PackageName { get; set; }
        public string? Description { get; set; }
        public string? FoodDetails { get; set; }
        public string? Rate { get; set; } 
        public string? DeparturePoint { get; set; }
        public string?  ArrivalPoint { get; set; }
        public List<ViaRoute>? ViaRoutes { get; set; }
        public List<Image>? Images { get; set; }
        public int AvailablityCount { get; set; }
        public int TotalDays { get; set; } 

    }
}
