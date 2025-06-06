﻿using Mapster;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;

namespace RentalOfPremises.Infrastructure.Mapping
{
    public class ModelToEntityAdvertMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<AdvertEntity, Advert>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Label, src => src.Label)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Responses, src => src.Responses)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.IsPublished, src => src.IsPublished)
                .Map(dest => dest.Owner, src => src.Owner)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.Premise, src => src.Premise)
                .Map(dest => dest.PremiseId, src => src.PremiseId)
                .Map(dest => dest.MainImageUrl, src => src.MainImageUrl)
                .Map(dest => dest.ListImageUrl, src => src.ListImageUrl)
                .MaxDepth(2)
                .TwoWays();

        }
    }
}
