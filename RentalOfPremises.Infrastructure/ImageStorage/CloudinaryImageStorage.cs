using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RentalOfPremises.Application.Interfaces;

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

        public async Task<string> UploadImage(IFormFile image, string path)
        {
            await using var memoryStream = new MemoryStream(); // Создаём поток в памяти
            await image.CopyToAsync(memoryStream);             // Копируем содержимое IFormFile в MemoryStream
            memoryStream.Seek(0, SeekOrigin.Begin);            // Перемещаем указатель потока в начало (сместить указатель на 0 байтов от начала потока)

            var uploadParams = new ImageUploadParams
            {
                Folder = path,
                File = new FileDescription(path, memoryStream),
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams); // Выполняем загрузку
            return uploadResult.SecureUrl.ToString();
        }

		/// <summary>
		/// Загружает в облако файл представленный массивом байтов. 
        /// Нужен в backgroud сервисе, так как IFormFile уничтожается с завершением http-запроса.
		/// </summary>
		/// <param name="fileBytes">Массив байтов файла.</param>
		/// <param name="path">Путь в облаке.</param>
		/// <returns>Url загруженного изображения.</returns>
		public async Task<string> UploadFileBytes(byte[] fileBytes, string path)
        {
            using var memoryStream = new MemoryStream(fileBytes);
			memoryStream.Seek(0, SeekOrigin.Begin);

			var uploadParams = new ImageUploadParams
            {
                Folder = path,
                File = new FileDescription(path, memoryStream),
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult?.SecureUrl?.ToString();

        }

        public async Task<bool> DeleteImageByUrl(string imageUrl, Guid userId)
        {
            var publicId = ExtractPublicIdFromUrl(imageUrl, userId);

            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Result != "ok")
            {
                //TODO: Написать свои кастомные исключения
                throw new InvalidOperationException("Cloudinary operation exception!");
            }

            return result.Result == "ok";
        }

        /*public async Task<bool> IsFolderEmpty(string folderPath)
        {
            var result = await _cloudinary.ListResourcesAsync(folderPath);

            return result.Resources.Any();
        }*/

        public async Task<bool> DeleteFolder(string folderPath)
        {
            var res = await _cloudinary.DeleteFolderAsync(folderPath);

            if(res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
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
