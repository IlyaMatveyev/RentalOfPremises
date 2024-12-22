namespace RentalOfPremises.Domain.Models
{
    public class ImageInAdvert
    {
        public Guid AdvertId { get; set; } = Guid.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public Advert Advert { get; set; } = new();
    }
}
