using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RentalOfPremises.Application.DTOs.AdvertDto
{
    public class ImageCollectionRequest
    {
        [Required]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
