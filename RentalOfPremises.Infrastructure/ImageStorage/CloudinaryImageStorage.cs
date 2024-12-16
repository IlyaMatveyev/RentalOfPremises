using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Infrastructure.ImageStorage
{
    public class CloudinaryImageStorage : IImageStorage
    {
        const long maxFileSize = 5 * 1024 * 1024; // 5 MB


        private readonly Cloudinary _cloudinary;

        public CloudinaryImageStorage(
            IOptions<CloudinarySettings> options
            )
        {
            var acc = new Account
            {
                Cloud = options.Value.CloudName,
                ApiKey = options.Value.ApiKey,
                ApiSecret = options.Value.ApiSecret
            };

            _cloudinary = new Cloudinary(acc);

        }

        public async Task<string> AddPremisesMainImage(IFormFile image, string path)
        {
            using var stream = image.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                Folder = path,
                File = new FileDescription(path, stream),
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }

        /*public async Task<string> UpdatePremisesMainImage(IFormFile newImage, string imageUrl, Guid userId) //TODO: Протестить
        {
            var isValid = ValidateImageFile(newImage);
            if (!isValid)
            {
                throw new ArgumentException("File is invalid!");
            }



            var publicId = ExtractPublicIdFromUrl(imageUrl, userId);
            using var stream = newImage.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(newImage.FileName, stream),
                PublicId = publicId,
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if(uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Image replacement failed: {uploadResult.Error?.Message}");
            }

            return uploadResult.SecureUrl.ToString();

        }*/

        public async Task<bool> DeleteImageByUrl(string imageUrl, Guid userId)
        {
            var publicId = ExtractPublicIdFromUrl(imageUrl, userId);

            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok";
        }

        public bool ValidateImageFile(IFormFile image)
        {
            if (image == null)
            {
                return false;
            }

            // Проверка размера файла
            if (image.Length > maxFileSize)
            {
                return false;
            }

            // Проверка расширения файла
            var allowedExtensions = new[] { ".jpg", ".jpeg" };

            var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
            }

            return true;
        }

        private string ExtractPublicIdFromUrl(string imageUrl, Guid userId)
        {
            // Преобразуем userId в строку
            string userIdString = userId.ToString();

            // Находим позицию, с которой начинается userId в URL
            int userIdIndex = imageUrl.IndexOf(userIdString);

            // Если userId найден в URL
            if (userIdIndex != -1)
            {
                // Извлекаем строку от userId до конца URL
                string pathAfterUserId = imageUrl.Substring(userIdIndex);

                // Убираем расширение файла, удаляя все символы после последней точки
                int extensionIndex = pathAfterUserId.LastIndexOf('.');
                if (extensionIndex != -1)
                {
                    pathAfterUserId = pathAfterUserId.Substring(0, extensionIndex);
                }

                return pathAfterUserId;
            }

            // Если userId не найден, возвращаем пустую строку или можно выбросить исключение
            return string.Empty;
        }
    }



}
