﻿using Mapster;
using RentalOfPremises.Application.DTOs.PremisesDto;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Mapping
{
    public class PremiseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //PremiseCreateRequest -> Premise
            TypeAdapterConfig<PremiseCreateRequest, Premises>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.CoutOfRooms, src => src.CoutOfRooms)
                .Map(dest => dest.Area, src => src.Area)
                .MaxDepth(2);

            //PremiseUpdateRequest -> Premise
            TypeAdapterConfig<PremisesUpdateRequest, Premises>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.CoutOfRooms, src => src.CoutOfRooms)
                .Map(dest => dest.Area, src => src.Area)
                .MaxDepth(2);

            TypeAdapterConfig<Premises, PremiseResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.CoutOfRooms, src => src.CoutOfRooms)
                .Map(dest => dest.Area, src => src.Area)
                .Map(dest => dest.MainImageUrl, src => src.MainImageUrl)
                .Map(dest => dest.OwnerName, src => src.Owner.UserName != null ? src.Owner.UserName : null)
                .Map(dest => dest.RenterName, src => src.Renter != null ? src.Renter.UserName : null);
        }
    }
}
