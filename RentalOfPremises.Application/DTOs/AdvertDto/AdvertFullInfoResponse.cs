using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.DTOs.AdvertDto
{
    public class AdvertFullInfoResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;

        public bool IsPublished { get; set; } = false;

        //главное фото объявления
        public string MainImageUrl { get; set; } = string.Empty;

        //коллекция фото
        public List<string> ImageUrlList { get; set; } = new();

        public int CountResponses { get; set; } = 0; //кол-во откликов

        //владелец
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;

        /*//помещение
        public Guid PremiseId { get; set; }
        public Premises Premise { get; set; }*/
    }
}
