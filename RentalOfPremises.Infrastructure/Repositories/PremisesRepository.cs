using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
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
        private readonly ICurrentUserContext _currentUserContext;
        public PremisesRepository(
            IRentalOfPremisesDbContext dbContext,
            IMapper mapper,
            IImageStorage imageStorage,
            ICurrentUserContext currentUserContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _imageStorage = imageStorage;
            _currentUserContext = currentUserContext;
        }


        public async Task<Guid> Add(Premises premise)
        {
            PremiseEntity premiseEntity = _mapper.Map<Premises, PremiseEntity>(premise);

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
        public async Task<Premises> ReadById(Guid premisesId)
        {
            var premiseEntity = await _dbContext.Premises
                .AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.Renter)
                .FirstOrDefaultAsync(p => p.Id == premisesId && p.OwnerId == _currentUserContext.UserId);

            if (premiseEntity == null)
            {
                throw new KeyNotFoundException("Premises not found!");
            }

            return _mapper.Map<PremiseEntity, Premises>(premiseEntity);
        }

        /// <summary>
        /// Метод получения всех помещений пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns name="List<Premise>">Список помещений</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Premises>?> ReadAll()
        {
            var premisesEntityList = await _dbContext.Premises
                .AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.Renter)
                .Where(p => p.OwnerId == _currentUserContext.UserId)
                .ToListAsync();

            if (premisesEntityList == null) { return null; }

            return _mapper.Map<List<PremiseEntity>, List<Premises>>(premisesEntityList);
        }

        /// <summary>
        /// Метод удаления помещения по ID
        /// </summary>
        /// <param name="id">ID помещения</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> Delete(Guid premisesId)
        {
            var imageUrl = await _dbContext.Premises
                .Where(p => p.Id == premisesId && p.OwnerId == _currentUserContext.UserId)
                .Select(p => p.MainImageUrl)
                .SingleOrDefaultAsync();

            //удаление mainImage
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var isDeleted = await _imageStorage.DeleteImageByUrl(imageUrl, _currentUserContext.UserId);
                if(isDeleted == false)
                {
                    throw new Exception("The Main Image could not be deleted when deleting the Premises object.");
                }
            }
            

            //Удаление Premises из БД
            var countOfDelitedObjects = await _dbContext.Premises
                .Where(p => p.Id == premisesId && p.OwnerId == _currentUserContext.UserId)
                .ExecuteDeleteAsync();

            return countOfDelitedObjects;
        }

        /// <summary>
        /// Метод изменения информации о помещении
        /// </summary>
        /// <param name="id">Идентификатор помещения, которое изменяем</param>
        /// <param name="premises">Обновлённая информация о помещении</param>
        /// <returns name="Guid">ID обновлённой записи</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Guid> Update(Guid premisesId, Premises premises)
        {
            var countOfUpdatedRows = await _dbContext.Premises
                .Where(p => p.Id == premisesId && p.OwnerId == _currentUserContext.UserId)
                .ExecuteUpdateAsync(property => property
                .SetProperty(p => p.Name, premises.Name)
                .SetProperty(p => p.Address, premises.Address)
                .SetProperty(p => p.Area, premises.Area)
                .SetProperty(p => p.CoutOfRooms, premises.CoutOfRooms)
                //.SetProperty(p => p.MainImageUrl, premises.MainImageUrl)
                );

            if (countOfUpdatedRows == 0)
            {
                throw new KeyNotFoundException("Error when trying to change the info of the Premises object");
            }

            return premisesId;
        }

        public async Task<Guid> UpdateMainImage(Guid premisesId, string newImageUrl)
        {
            var countOfUpdatedRows = await _dbContext.Premises
                .Where(p => p.Id == premisesId && p.OwnerId == _currentUserContext.UserId)
                .ExecuteUpdateAsync(property => property
                .SetProperty(p => p.MainImageUrl, newImageUrl));

            if (countOfUpdatedRows == 0)
            {
                throw new KeyNotFoundException("Error when trying to change the main photo of the Premises object!");
            }

            return premisesId;
        }
    }
}
