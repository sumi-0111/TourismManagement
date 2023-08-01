using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismApp.Models
{
    public class Traveller
    {
        [Key]
        public int TravellerId { get; set; }

        [ForeignKey("TravellerId")]
        public User? User { get; set; }
        public string? TravellerName { get; set; }
        public string TravellerEmail { get; set; }
        public string? TravellerPhoneNo { get; set; }   
        public string? TravellerGender { get; set; }
        public string? TravellerAddress { get; set; }
        public int TravellerAge { get; set; }



    }
}
