using Microsoft.AspNetCore.Http;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IImageStorage
    {
        //методы для MainImage в Premises
        public Task<string> AddPremisesMainImage(IFormFile image, string path);
        public Task<bool> DeleteImageByUrl(string imageUrl, Guid userId);

        //методы для Advert:

        //добавить кучу фоток разом (до 50)
        //удалить всю коллекцию

        //добавить фото в коллекцию
        //удалить фото из коллекции

        public bool ValidateImageFile(IFormFile image);
    }
}
