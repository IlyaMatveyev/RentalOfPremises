﻿using Mapster;

namespace RentalOfPremises.Application.Mapping
{
    public class AdvertMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Тут будет маппинг из DTO в Model и наоборот


            /*TypeAdapterConfig<AdvertEntity, Advert>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Label, src => src.Label)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Responses, src => src.Responses)
                .Map(dest => dest.Owner, src => src.Owner)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.Premise, src => src.Premise)
                .Map(dest => dest.Premise, src => src.Premise)
                .Map(dest => dest.PremiseId, src => src.PremiseId)
                .TwoWays();*/

        }
    }
}
