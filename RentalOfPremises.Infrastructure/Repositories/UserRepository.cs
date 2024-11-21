using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Domain.Interfaces;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.MSSQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalOfPremises.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRentalOfPremisesDbContext _dbContext;
        public UserRepository(IRentalOfPremisesDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Guid> Create(User user)
        {
            //TODO: тут или в сервисе скорее всего нужно будет прописать нормальную работу с регистрацией пользователя
            
            //TODO: возможно правильнее здесь сделать маппинг в UserEntity и в целом на уровне работы с БД использовать отдельные модели Entity

            /*await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();*/

            return user.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<IEnumerable<User>> ReadAll()
        {
            /*return await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();*/
            return new List<User>();
        }

        public async Task<User?> ReadById(Guid id)
        {
            /*return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);*/
            return null;
        }

        public async Task<Guid> Update(User user)
        {
            await _dbContext.Users
                .Where(u => u.Id == user.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.UserName, u => user.UserName)
                    .SetProperty(u => u.Email, u => user.Email)
                    .SetProperty(u => u.PasswordHash, u => user.PasswordHash)
                    .SetProperty(u => u.IsBanned, u => user.IsBanned));

            return user.Id;
        }
    }
}
