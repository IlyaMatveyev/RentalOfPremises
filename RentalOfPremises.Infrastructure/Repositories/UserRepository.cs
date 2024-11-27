using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;
using RentalOfPremises.Infrastructure.MSSQLServer;

namespace RentalOfPremises.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRentalOfPremisesDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(IRentalOfPremisesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<Guid> Create(User user)
        {
            //TODO: тут или в сервисе скорее всего нужно будет прописать нормальную работу с регистрацией пользователя

            //маппинг
            var userEntity = _mapper.Map<User, UserEntity>(user);


            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

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
            return await _dbContext.Users
                .AsNoTracking()
                .ProjectToType<User>()  //маппинг из UserEntity в User
                .ToListAsync();         //материализация объектов (IQueryable -> IEnumirable)
        }

        public async Task<User?> ReadByEmail(string email)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .ProjectToType<User?>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> ReadById(Guid id)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .ProjectToType<User?>()
                .FirstOrDefaultAsync(u => u.Id == id);
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
