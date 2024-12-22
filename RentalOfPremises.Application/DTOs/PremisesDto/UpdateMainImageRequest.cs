using Microsoft.AspNetCore.Http;

namespace RentalOfPremises.Application.DTOs.PremisesDto
{
    public class UpdateMainImageRequest
    {
        public IFormFile newImage { get; set; }
    }
}
