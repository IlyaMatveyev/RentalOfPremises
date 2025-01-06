using Mapster;
using RentalOfPremises.Application.DTOs.AdvertDto;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Mapping
{
    public class AdvertMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //AdvertCreateRequest -> Advert
            TypeAdapterConfig<AdvertCreateRequest, Advert>
                .NewConfig()
                .Map(dest => dest.Label, src => src.Label)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.PremiseId, src => src.PremiseId)
                .MaxDepth(2);

            TypeAdapterConfig<Advert, AdvertFullInfoResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Label, src => src.Label)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.IsPublished, src => src.IsPublished)
                .Map(dest => dest.MainImageUrl, src => src.MainImageUrl)
                .Map(dest => dest.ListImageUrl, src => src.ListImageUrl)
                .Map(dest => dest.CountResponses, src => src.Responses != null ? src.Responses.Count : 0)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.OwnerName, src => src.Owner.UserName)
                .Map(dest => dest.PremiseId, src => src.PremiseId)
                .Map(dest => dest.Premise, src => src.Premise)
                .MaxDepth(2);

            TypeAdapterConfig<AdvertUpdateInfoRequest, Advert>
                .NewConfig()
                .Map(dest => dest.Label, src => src.Label)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .MaxDepth(2);

            TypeAdapterConfig<Advert, AdvertShortInfoResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Label, src => src.Label)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.Address, src => src.Premise.Address)
                .Map(dest => dest.CoutOfRooms, src => src.Premise.CoutOfRooms)
                .Map(dest => dest.Area, src => src.Premise.Area)
                .Map(dest => dest.MainImageUrl, src => src.MainImageUrl)
                .MaxDepth(2);
        }
    }
}
