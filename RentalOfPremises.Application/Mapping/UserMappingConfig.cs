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
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<UserEntity, User>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PasswordHash, src => src.PasswordHash)
                .Map(dest => dest.IsEmailConfirmed, src => src.IsEmailConfirmed)
                .Map(dest => dest.IsBanned, src => src.IsBanned)
                .Map(dest => dest.PersonalPremises, src => src.PersonalPremises)
                .Map(dest => dest.Adverts, src => src.Adverts)
                .Map(dest => dest.Responses, src => src.Responses)
                .Map(dest => dest.RentedPremises, src => src.RentedPremises)
                .TwoWays();
        }
    }
}
