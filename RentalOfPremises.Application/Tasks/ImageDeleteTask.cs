namespace RentalOfPremises.Application.Tasks
{
    public class ImageDeleteTask
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid AdvertId { get; set; } = Guid.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
