using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Application.DTOs.Pagination;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;
using RentalOfPremises.Infrastructure.MSSQLServer;

namespace RentalOfPremises.Infrastructure.Repositories
{
    public class AdvertsRepository : IAdvertsRepository
    {
        private readonly IRentalOfPremisesDbContext _dbContext;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IMapper _mapper;
        public AdvertsRepository(
            IRentalOfPremisesDbContext dbContext,
            ICurrentUserContext currentUserContext,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _currentUserContext = currentUserContext;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<Advert>>ReadAll(PaginationParams paginationParams, Guid? userId = null)
        {
            var query = _dbContext.Adverts.Include(a => a.Premise).AsNoTracking().AsQueryable();

            //реализация для чтения всех опубликованных объявлений
            if(userId == null)
            {
                query = query.Where(a => a.IsPublished == true)
                    .Skip((paginationParams.PageNumder - 1) * paginationParams.PageSize)
                    .Take(paginationParams.PageSize);
            }
            else //реализация для чтения всех своих объявлений
            {
                query = query.Where(a => a.OwnerId == userId)
                    .Skip((paginationParams.PageNumder - 1) * paginationParams.PageSize)
                    .Take(paginationParams.PageSize);
            }

            var listAdvertEntity = await query.ToListAsync();
            if(listAdvertEntity.Count < 1)
            {
                throw new KeyNotFoundException("Adverts not found");
            }

            var totalCount = await query.CountAsync(); // Общее количество записей

            return new PaginatedResult<Advert> 
            { 
                Items = _mapper.Map<List<Advert>>(listAdvertEntity), 
                TotalCount = totalCount,
                PageNumder = paginationParams.PageNumder,
                PageSize = paginationParams.PageSize
            };

        }

        public async Task<List<Advert>> ReadAllNonPaginated(Guid userId)
        {
            var listAdvertEntity = await _dbContext.Adverts.AsNoTracking().Where(a => a.OwnerId == userId).ToListAsync();

            if (listAdvertEntity.Count < 1)
            {
                throw new KeyNotFoundException("Adverts not found");
            }

            return _mapper.Map<List<Advert>>(listAdvertEntity);
        }


        public async Task<Guid> Add(Advert advert)
        {
            var premisesEntity = await _dbContext.Premises
                .Include(p => p.Advert)
                .FirstOrDefaultAsync(p => p.Id == advert.PremiseId && p.OwnerId == advert.OwnerId);

            if(premisesEntity != null && premisesEntity.Advert == null)
            {
                var advertEntity = _mapper.Map<AdvertEntity>(advert);

                await _dbContext.Adverts.AddAsync(advertEntity);
                await _dbContext.SaveChangesAsync();

                return advertEntity.Id;
            }

            // Если проверки не пройдены
            throw new KeyNotFoundException();
        }

        
        public async Task<Advert> ReadById(Guid advertId, Guid? userId = null)
        {
            IQueryable<AdvertEntity> query;

            //реализация для обращения владельца объявления
            if (userId.HasValue)
            {
                query = _dbContext.Adverts
                .AsNoTracking()
                .Include(p => p.ListImageUrl)
                .Include(p => p.Responses)
                .Include(p => p.Owner)
                .Include(p => p.Premise)
                .Where(a => a.Id == advertId && a.OwnerId == userId);
            }
            else //реализация НЕ владельца (чтение опубликованного объявления)
            {
                query = _dbContext.Adverts
                .AsNoTracking()
                .Include(p => p.ListImageUrl)
                .Include(p => p.Responses)
                .Include(p => p.Owner)
                .Include(p => p.Premise)
                .Where(a => a.IsPublished == true);
            }

            var advertEntity = await query.FirstOrDefaultAsync();

            if (advertEntity == null)
            {
                throw new KeyNotFoundException();
            }

            //mapping
            return _mapper.Map<Advert>(advertEntity);
        }

        public async Task<Guid> UpdateMainImage(string mainImageUrl, Guid advertId)
        {
            var countUpdatedRows = await _dbContext.Adverts
                .Where(a => a.OwnerId == _currentUserContext.UserId && a.Id == advertId)
                .ExecuteUpdateAsync(prop => prop
                .SetProperty(a => a.MainImageUrl, mainImageUrl));

            if (countUpdatedRows < 1)
            {
                throw new KeyNotFoundException("Error when trying to change the MainImageUrl of the Advert object.");
            }

            return advertId;
        }

        public async Task<int> Delete(Guid advertId)
        {
            var countOfDdeletedRows = await _dbContext.Adverts
                .Where(a => a.Id == advertId && a.OwnerId == _currentUserContext.UserId)
                .ExecuteDeleteAsync();

            if(countOfDdeletedRows == 0)
            {
                throw new KeyNotFoundException("Error when trying to delete the Advert object.");
            }

            return countOfDdeletedRows;
        }

        public async Task<Guid> PublishUnpublish(Guid advertId)
        {
            var countOfUpdatedRows = await _dbContext.Adverts
                .Where(a => a.OwnerId == _currentUserContext.UserId && a.Id == advertId)
                .ExecuteUpdateAsync(setPropertyCalls => setPropertyCalls
                                    .SetProperty(a => a.IsPublished, a=> !a.IsPublished));

            if(countOfUpdatedRows == 0)
            {
                throw new KeyNotFoundException("Error when trying to change the info (Publish/Unpublish) of the Advert object.");
            }

            return advertId;
        }

        public async Task<Guid> UpdateInfo(Advert advert, Guid advertId)
        {
            var countOfUpdatedRows = await _dbContext.Adverts
                .Where(a => a.OwnerId == _currentUserContext.UserId && a.Id == advertId)
                .ExecuteUpdateAsync(prop => prop
                .SetProperty(a => a.Label, advert.Label)
                .SetProperty(a=>a.Description, advert.Description)
                .SetProperty(a=>a.Price, advert.Price));

            if(countOfUpdatedRows < 1)
            {
                throw new KeyNotFoundException("Error when trying to change the info of the Advert object.");
            }


            return advertId;
        }
    }
}
