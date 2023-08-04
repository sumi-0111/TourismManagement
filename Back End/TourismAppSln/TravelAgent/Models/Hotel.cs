﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TourPackage.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        public int ItineraryId { get; set; }
        [ForeignKey("ItineraryId")]
        [JsonIgnore]
        public Itinerary? Itinerary { get; set; }
        public string? HotelName { get; set; }
        public int HotelRating { get; set; }

        [Required]
        public string? RoomType { get; set; }

        [Required] 
        public string? HotelFood { get; set; }
        public string? HotelImage { get; set; }
    } 
}
