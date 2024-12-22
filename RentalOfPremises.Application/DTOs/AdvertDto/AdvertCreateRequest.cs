using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.DTOs.AdvertDto
{
    public class AdvertCreateRequest
    {
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;

        //помещение
        public Guid PremiseId { get; set; }
    }
}
