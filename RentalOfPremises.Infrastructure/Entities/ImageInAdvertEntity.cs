namespace RentalOfPremises.Infrastructure.Entities
{
    public class ImageInAdvertEntity
    {
        public Guid AdvertId { get; set; } = Guid.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public AdvertEntity AdvertEntity { get; set; } = null!;
    }
}
