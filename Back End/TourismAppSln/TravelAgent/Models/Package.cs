using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        [Required]
        public string? PackageName { get; set; }

        [Required]
        public string? TravelAgencyName { get; set; }

      
        public string? Description { get; set; }

        [Required]
        public double Rate { get; set; }

        [Required]
        public string? DeparturePoint { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        [Required]
        public string? ArrivalPoint { get; set; }
        public int AvailablityCount { get; set; }
        public int TotalDays { get; set; }

        [Required]
        public string? Transportation { get; set; }
        public string? PackageImage { get; set; }
        public ContactDetails ContactDetails { get; set; }
    }
}
