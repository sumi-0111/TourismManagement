using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TourImages.Models
{
    public class UserModel
    {

        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}