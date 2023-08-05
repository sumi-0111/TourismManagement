using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models
{
    public class Images
    {
        [Key]
        public int ImageId { get; set; }
        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public Package? Package { get; set; }
        public string? PackageName { get; set; }
        public string? ImageUrl { get; set; } 
    }
}
