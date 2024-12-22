using RentalOfPremises.Application.DTOs.UserDto;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IUserService
    {
        public Task<Guid> Register(UserRegisterRequest userRequest);
        public Task<string> Login(UserLoginRequest userRequest);
        public Task<User> GetByEmail(string email);


    }
}
