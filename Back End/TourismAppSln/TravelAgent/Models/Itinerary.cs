using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TourPackage.Models
{
    public class Itinerary
    {
        [Key]
        public int ItineraryId { get; set; }

        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        [JsonIgnore]
        public Package? Package { get; set; }

        [Required]
        public string? PackageName { get; set; }
        [Required]
        public  string? DayandVisit { get; set; }
        [Required]
        public string? DestinationName { get; set; }

        public string DestinationDescription { get; set; }
        [Required]
        public string? FoodDetails { get; set; } 
        public string? ItineraryImage { get; set; }
       

        public List<Hotel>? Hotels { get; set; }

    } 
}
