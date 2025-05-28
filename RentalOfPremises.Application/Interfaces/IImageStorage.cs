using Microsoft.AspNetCore.Http;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IImageStorage
    {
        public Task<string> UploadImage(IFormFile image, string path);
        public Task<string> UploadFileBytes(byte[] fileBytes, string path);

		public Task<bool> DeleteImageByUrl(string imageUrl, Guid userId);

        Task<bool> DeleteFolder(string folderPath); ////////////////

        public bool ValidateImageFile(IFormFile image);
    }
}
