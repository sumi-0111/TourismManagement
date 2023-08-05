using System.ComponentModel.DataAnnotations;

namespace FeedBackApi.Models
{
    public class FeedBack
    {
        [Key]
        public int FeedbackID { get; set; }
        public int TravellerId { get; set; }
        public int? PackageId { get; set; }
        [Required]
        public string? Comment { get; set; }
        [Required]
        public double? Ratings { get; set; }
    }
}
