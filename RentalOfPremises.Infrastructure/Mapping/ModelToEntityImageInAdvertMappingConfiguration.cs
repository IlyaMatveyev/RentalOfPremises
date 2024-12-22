using Mapster;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;

namespace RentalOfPremises.Infrastructure.Mapping
{
    public class ModelToEntityImageInAdvertMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<ImageInAdvert, ImageInAdvertEntity>
                .NewConfig()
                .Map(dest => dest.AdvertId, src => src.AdvertId)
                .Map(dest => dest.AdvertId, src => src.Advert)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl)
                .MaxDepth(2)
                .TwoWays();

        }
    }
}
