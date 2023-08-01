﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourPackage.Models
{
    public class ViaRoute
    {
        [Key]
        public int Id { get; set; }

        

        public string? ViaRouteDetail { get; set; }
        public TourPackage TourPackage { get; set; }

    }
}