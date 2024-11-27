using Mapster;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Application.Mapping
{
    public class AdvertMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<AdvertEntity, Advert>
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
                .TwoWays();

        }
    }
}
