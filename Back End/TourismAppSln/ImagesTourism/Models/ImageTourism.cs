using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ImagesTourism.Models
{
    public class ImageTourism
    {
        [Key]
        public int ImageId { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}

