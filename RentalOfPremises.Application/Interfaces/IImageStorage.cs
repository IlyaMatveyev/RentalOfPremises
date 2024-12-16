using Microsoft.AspNetCore.Http;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IImageStorage
    {
        public Task<string> AddPremisesMainImage(IFormFile image, string path);
        //public Task<string> UpdatePremisesMainImage(IFormFile newImage, string imageUrl, Guid userId);
        public Task<bool> DeleteImageByUrl(string imageUrl, Guid userId);

        public bool ValidateImageFile(IFormFile image);
    }
}
