using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;
using RentalOfPremises.Infrastructure.MSSQLServer;

namespace RentalOfPremises.Infrastructure.Repositories
{
    public class PremisesRepository : IPremisesRepository
    {
        private readonly IRentalOfPremisesDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IImageStorage _imageStorage;
        public PremisesRepository(
            IRentalOfPremisesDbContext dbContext,
            IMapper mapper,
            IImageStorage imageStorage)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _imageStorage = imageStorage;
        }


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
        public async Task<Premise> ReadById(Guid premisesId, Guid userId)
        {
            var premiseEntity = await _dbContext.Premises
                .AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.Renter)
                .FirstOrDefaultAsync(p => p.Id == premisesId && p.OwnerId == userId);

            if (premiseEntity == null)
            {
                throw new KeyNotFoundException("Premises not found!");
            }

            return _mapper.Map<PremiseEntity, Premise>(premiseEntity);
        }

        /// <summary>
        /// Метод получения всех помещений пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns name="List<Premise>">Список помещений</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Premise>?> ReadAll(Guid userId)
        {
            var premisesEntityList = await _dbContext.Premises
                .AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.Renter)
                .Where(p => p.OwnerId == userId)
                .ToListAsync();

            if (premisesEntityList == null) { return null; }

            return _mapper.Map<List<PremiseEntity>, List<Premise>>(premisesEntityList);
        }

        /// <summary>
        /// Метод удаления помещения по ID
        /// </summary>
        /// <param name="id">ID помещения</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> Delete(Guid premisesId, Guid userId)
        {
            var imageUrl = await _dbContext.Premises
                .Where(p => p.Id == premisesId && p.OwnerId == userId)
                .Select(p => p.MainImageUrl)
                .SingleOrDefaultAsync();

            //удаление mainImage
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var isDeleted = await _imageStorage.DeleteImageByUrl(imageUrl, userId);
                if(isDeleted == false)
                {
                    throw new Exception("The Main Image could not be deleted when deleting the Premises object.");
                }
            }
            

            //Удаление Premises из БД
            var countOfDelitedObjects = await _dbContext.Premises
                .Where(p => p.Id == premisesId && p.OwnerId == userId)
                .ExecuteDeleteAsync();

            return countOfDelitedObjects;
        }

        /// <summary>
        /// Метод изменения информации о помещении
        /// </summary>
        /// <param name="id">Идентификатор помещения, которое изменяем</param>
        /// <param name="premise">Обновлённая информация о помещении</param>
        /// <returns name="Guid">ID обновлённой записи</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Guid> Update(Guid premisesId, Premise premises, Guid userId)
        {
            var countOfUpdatedRows = await _dbContext.Premises
                .Where(p => p.Id == premisesId && p.OwnerId == userId)
                .ExecuteUpdateAsync(property => property
                .SetProperty(p => p.Name, premises.Name)
                .SetProperty(p => p.Address, premises.Address)
                .SetProperty(p => p.Area, premises.Area)
                .SetProperty(p => p.CoutOfRooms, premises.CoutOfRooms)
                .SetProperty(p => p.MainImageUrl, premises.MainImageUrl)
                );

            if (countOfUpdatedRows == 0)
            {
                return Guid.Empty;
            }

            return premisesId;
        }

    }
}
