﻿using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Infrastructure.Entities;
using RentalOfPremises.Infrastructure.MSSQLServer;

namespace RentalOfPremises.Infrastructure.Repositories
{
    public class ImagesInAdvertRepository : IImagesInAdvertRepository
    {
        private readonly IRentalOfPremisesDbContext _dbContext;
        public ImagesInAdvertRepository(IRentalOfPremisesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Guid advertId, string imageUrl)
        {
            var entity = new ImageInAdvertEntity{ AdvertId = advertId, ImageUrl = imageUrl };
            await _dbContext.ImagesInAdverts.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity.AdvertId;
        }

        public async Task<int> Delete(Guid advertId, string imageUrl)
        {
            var countOfDeletedRows = await _dbContext.ImagesInAdverts
                .Where(i => i.AdvertId == advertId && i.ImageUrl == imageUrl)
                .ExecuteDeleteAsync();

            return countOfDeletedRows;
        }
    }
}
