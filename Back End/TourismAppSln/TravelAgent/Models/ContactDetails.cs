using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TourPackage.Models 
{
    public class ContactDetails
    {
        [Key]
        public int ContactId { get; set; }

        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public Package? Package { get; set; } 
        public string? AgentName { get; set; } 
        public string? AgentPhoneNo { get; set; } 
        public string? AgentEmail { get; set; } 

        
    }

}
    