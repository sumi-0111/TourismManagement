using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models
{
    public class ContactDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TravelAgentId")]
        public int TravelAgentId { get; set; }
        public string? TravelAgentName { get; set; }
        public string? Phone { get; set; }
        public string? Email  { get; set; }
    }
}
