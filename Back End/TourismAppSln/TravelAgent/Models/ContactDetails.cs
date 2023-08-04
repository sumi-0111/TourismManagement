using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TourPackage.Models 
{
    public class ContactDetails
    {
        [Key]
        public int ContactId { get; set; } 

        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        [JsonIgnore]
        public Package? Package { get; set; } 
        public string? TravelAgentName { get; set; } 
        public string? Phone { get; set; } 
        public string? Email  { get; set; } 
        public string? MapImage { get; set;} 

        
    }

}
    