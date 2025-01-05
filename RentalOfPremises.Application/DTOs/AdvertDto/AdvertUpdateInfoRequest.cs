namespace RentalOfPremises.Application.DTOs.AdvertDto
{
    public class AdvertUpdateInfoRequest
    {
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
    }
}
