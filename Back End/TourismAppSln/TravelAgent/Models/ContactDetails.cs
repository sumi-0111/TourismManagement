using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models 
{
    public class ContactDetails
    {
        [Key]
        public int ContactId { get; set; } 

        [ForeignKey("TourPackage")] 
        public int PackageId { get; set; } 
        public Package Package { get; set; } 
        public string? TravelAgentName { get; set; } 
        public string? Phone { get; set; } 
        public string? Email  { get; set; } 
        public string? MapImage { get; set;}
    }

}
    