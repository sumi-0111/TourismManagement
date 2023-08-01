using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TourismApp.Models;

namespace TourismApp.Models
{
    public class TravelAgent
    {
       
        [Key]
        public int TravelAgentId { get; set; }
           
        [ForeignKey("TravelAgentId")]
        public User? User { get; set; }
        public string TravelAgentName { get; set; }
        public string? TravelAgentEmail { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public string? TravelAgentPhoneNo { get; set; } 
        public string TravelAgentStatus { get; set; }
    }
}
