﻿using Mapster;

namespace RentalOfPremises.Application.Mapping
{
    public class PremiseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //TODO: Прописать маппинг для DTO
            //Тут будет маппинг из DTO в Model и наоборот

            /*TypeAdapterConfig<PremiseEntity, Premise>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.CoutOfRooms, src => src.CoutOfRooms)
                .Map(dest => dest.Area, src => src.Area)
                .Map(dest => dest.Owner, src => src.Owner)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.Renter, src => src.Renter)
                .Map(dest => dest.RenterId, src => src.RenterId)
                .Map(dest => dest.Advert, src => src.Advert)
                .TwoWays();*/
        }
    }
}
