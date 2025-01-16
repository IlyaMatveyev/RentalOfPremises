using Microsoft.AspNetCore.Http;

namespace RentalOfPremises.Application.Tasks
{
    public class ImageUploadTask
    {
        public Guid AdvertId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string PathInCloud { get; set; } = string.Empty;
    }
}
