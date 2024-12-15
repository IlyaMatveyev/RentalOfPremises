using Microsoft.AspNetCore.Http;

namespace RentalOfPremises.Application.DTOs
{
    public class UpdateMainImageRequest
    {
        public IFormFile newImage {  get; set; }
    }
}
