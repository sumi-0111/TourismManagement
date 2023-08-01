using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models
{
    public class Image
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("TravelAgentId")]
        public string? TourPackageId { get; set; } 
        public string? ImageUrl { get; set; }
    }
}
