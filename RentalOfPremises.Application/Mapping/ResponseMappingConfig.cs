﻿using Mapster;

namespace RentalOfPremises.Application.Mapping
{
    public class ResponseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //Тут будет маппинг из DTO в Model и наоборот

            /*TypeAdapterConfig<ResponseEntity, ResponseEntity>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest.Sender, src => src.Sender)
                .Map(dest => dest.SenderId, src => src.SenderId)
                .Map(dest => dest.Advert, src => src.Advert)
                .Map(dest => dest.AdvertId, src => src.AdvertId)
                .TwoWays();*/
        }
    }
}
