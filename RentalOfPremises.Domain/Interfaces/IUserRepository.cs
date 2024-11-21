using RentalOfPremises.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<Guid> Create(User user);
        public Task<Guid> Update(User user);
        public Task<Guid> Delete(Guid id);
        public Task<IEnumerable<User>> ReadAll();
        public Task<User?> ReadById(Guid id);

    }
}
