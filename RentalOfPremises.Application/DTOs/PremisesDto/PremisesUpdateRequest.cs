﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RentalOfPremises.Application.DTOs.PremisesDto
{
    public class PremisesUpdateRequest
    {
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public int CoutOfRooms { get; set; } = 0;

        [Required]
        public double Area { get; set; } = 0;

    }
}
