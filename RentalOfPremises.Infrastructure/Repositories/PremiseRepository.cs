using MapsterMapper;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;
using RentalOfPremises.Infrastructure.MSSQLServer;

namespace RentalOfPremises.Infrastructure.Repositories
{
    public class PremiseRepository : IPremiseRepository
    {
        private readonly IRentalOfPremisesDbContext _dbContext;
        private readonly IMapper _mapper;
        public PremiseRepository(
            IRentalOfPremisesDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /*
         Необходимые методы:
        1) Добавление Помещения Пользователя;
        2) Считывание всех Помещений конкретного пользователя
        3) Считывание всех Помещений пользователя
        4) Удаление Помещения пользователя по Id
        5) Изменение Помещения пользователя по Id
        */

        public async Task<Guid> Add(Premise premise)
        {
            PremiseEntity premiseEntity = _mapper.Map<Premise, PremiseEntity>(premise);

            await _dbContext.Premises.AddAsync(premiseEntity);
            await _dbContext.SaveChangesAsync();

            return premise.Id;
        }

        /// <summary>
        /// Метод, который позволяет получить информацию о помещении из БД по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор помещения</param>
        /// <returns>Model.Premise</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Premise> ReadById(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод получения всех помещений пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns name="List<Premise>">Список помещений</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Premise>> ReadAll(Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод удаления помещения по ID
        /// </summary>
        /// <param name="id">ID помещения</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод изменения информации о помещении
        /// </summary>
        /// <param name="id">Идентификатор помещения, которое изменяем</param>
        /// <param name="premise">Обновлённая информация о помещении</param>
        /// <returns name="Guid">ID обновлённой записи</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Guid> Update(Guid id, Premise premise)
        {
            throw new NotImplementedException();
        }

    }
}
