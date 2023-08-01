using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("TourPackage")]
        public int TourId { get; set; }
        public string? ImageUrl { get; set; }
        public TourPackage TourPackage { get; set; }
    }
}
