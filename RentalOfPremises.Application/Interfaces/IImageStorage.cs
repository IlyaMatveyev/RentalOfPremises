using Microsoft.AspNetCore.Http;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IImageStorage
    {
        public Task<string> AddPremisesMainImage(IFormFile image, string path);
        public Task<string> UpdatePremisesMainImage(Guid userId, IFormFile newImage, string imageUrl);
        public Task<bool> DeleteImageByUrl(string imageUrl, Guid userId);

        public bool ValidateImageFile(IFormFile image);
    }
}
