using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TourPackage.Models
{
    public class Itinerary
    {
        [Key]
        public int ItineraryId { get; set; }

        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public Package? Package { get; set; }

        [Required]
        public  string? DayandVisit { get; set; }
        [Required]
        public string? DestinationName { get; set; }
        [Required]
        public string? DestinationDescription { get; set; }
        public Images? Images { get; set; }



    } 
}
