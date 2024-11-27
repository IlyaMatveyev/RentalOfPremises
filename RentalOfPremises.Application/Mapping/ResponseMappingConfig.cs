using Mapster;
using RentalOfPremises.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Application.Mapping
{
    public class ResponseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<ResponseEntity, ResponseEntity>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest.Sender, src => src.Sender)
                .Map(dest => dest.SenderId, src => src.SenderId)
                .Map(dest => dest.Advert, src => src.Advert)
                .Map(dest => dest.AdvertId, src => src.AdvertId)
                .TwoWays();
        }
    }
}
