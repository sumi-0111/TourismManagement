using System.ComponentModel.DataAnnotations;

namespace TravellerFeedBackAPI.Models
{
    public class UserFeedBack
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
