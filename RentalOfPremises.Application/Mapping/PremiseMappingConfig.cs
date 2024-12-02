using Mapster;
using RentalOfPremises.Application.DTOs;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Mapping
{
    public class PremiseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //PremiseCreateRequest -> Premise
            TypeAdapterConfig<PremiseCreateRequest, Premise>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.CoutOfRooms, src => src.CoutOfRooms)
                .Map(dest => dest.Area, src => src.Area)
                .MaxDepth(2);
        }
    }
}
