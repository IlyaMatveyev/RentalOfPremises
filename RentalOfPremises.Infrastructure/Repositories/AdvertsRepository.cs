﻿using MapsterMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> Delete(Guid advertId)
        {
            var countOfDdeletedRows = await _dbContext.Adverts
                .Where(a => a.Id == advertId && a.OwnerId == _currentUserContext.UserId)
                .ExecuteDeleteAsync();

            if(countOfDdeletedRows == 0)
            {
                throw new KeyNotFoundException();
            }

            return countOfDdeletedRows;
        }
    }
}
