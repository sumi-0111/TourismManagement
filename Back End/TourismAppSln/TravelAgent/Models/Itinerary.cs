using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models
{
    public class Itinerary
    {
        [Key]
        public int ItineraryId { get; set; }

        [ForeignKey("Package")]
        public int PackageId { get; set; } 

        public Package Package { get; set; }

        [Required]
        public string? PackageName { get; set; }
        [Required]
        public  string? DayandVisit { get; set; }
        [Required]
        public string? DestinationDescription { get; set; }
        [Required]
        public string? FoodDetails { get; set; }
      

        public List<Hotel> Hotels { get; set; }

    } 
}
