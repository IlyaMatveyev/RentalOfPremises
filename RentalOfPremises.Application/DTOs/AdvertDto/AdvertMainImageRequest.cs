using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RentalOfPremises.Application.DTOs.AdvertDto
{
    public class AdvertMainImageRequest
    {
        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}
