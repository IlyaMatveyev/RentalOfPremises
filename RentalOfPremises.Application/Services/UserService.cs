using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.ReadByEmail(email);
        }

        public Guid Login(User user)
        {
            throw new NotImplementedException();
        }

        public Guid Register(User user)
        {
            throw new NotImplementedException();
        }

        Task<Guid> IUserService.Login(User user)
        {
            throw new NotImplementedException();
        }

        Task<Guid> IUserService.Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
