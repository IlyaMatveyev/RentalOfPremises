using RentalOfPremises.Application.DTOs.UserDto;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Application.Services
{
    public class UsersService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJWTProvider _jwtProvider;
        public UsersService(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher,
            IJWTProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Guid> Register(UserRegisterRequest userRequest)
        {
            //не занят ли email
            var isEmailUnique = await _userRepository.CheckEmailUnique(userRequest.Email);
            if (!isEmailUnique)
            {
                return Guid.Empty;
            }

            //если email свободен
            var hashedPassword = _passwordHasher.Generate(userRequest.Password);
            var user = new User(userRequest.UserName, hashedPassword, userRequest.Email);

            return await _userRepository.Create(user);
        }

        public async Task<string> Login(UserLoginRequest userRequest)
        {
            //проверка есть ли User в БД
            var userFromDB = await _userRepository.ReadByEmail(userRequest.Email);
            if (userFromDB == null)
            {
                throw new Exception("Email not found!");
            }

            //проверка правильности пароля
            var result = _passwordHasher.Verify(userRequest.Password, userFromDB.PasswordHash);
            if(result == false)
            {
                throw new Exception("Wrong password!");
            }

            //генерируем токен 
            var jwtToken = _jwtProvider.GenerateToken(userFromDB);

            return jwtToken;
        }
        
        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.ReadByEmail(email);
        }

        
    }
}
