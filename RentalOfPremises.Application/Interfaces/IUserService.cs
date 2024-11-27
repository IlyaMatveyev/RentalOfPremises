using RentalOfPremises.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IUserService
    {
        public Task<Guid> Register(User user);
        public Task<Guid> Login(User user);
        public Task<User> GetByEmail(string email);
    }
}
