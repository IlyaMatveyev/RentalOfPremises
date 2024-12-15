using Microsoft.AspNetCore.Http;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Services
{
    public class PremisesService : IPremisesService
    {
        private readonly IPremisesRepository _premiseRepository;
        private readonly IImageStorage _imageStorage;
        public PremisesService(
            IPremisesRepository premiseRepository, 
            IImageStorage imageStorage
            )
        {
            _premiseRepository = premiseRepository;
            _imageStorage = imageStorage;
        }

        public async Task<Guid> Add(Premise premise, Guid userId)
        {
            premise.OwnerId = userId;

            return await _premiseRepository.Add(premise);
        }

        public async Task<Premise?> GetById(Guid premisesId, Guid userId)
        {
            var premise = await _premiseRepository.ReadById(premisesId, userId);
            
            return premise;
        }

        public async Task<List<Premise>?> GetAll(Guid userId)
        {
            return await _premiseRepository.ReadAll(userId);
        }

        public async Task<int> Delete(Guid premisId, Guid userId)
        {
            //TODO: Тут нужно будет вытащить Premis по id и проверить OwnerId с userId
            var countOfDelitedObjects = await _premiseRepository.Delete(premisId, userId);

            return countOfDelitedObjects;
        }

        public async Task<Guid> Update(Guid premisId, Premise premises, Guid userId)
        {
            return await _premiseRepository.Update(premisId, premises, userId);
        }

        public async Task<Guid> UpdateMainImage(Guid premisesId, IFormFile newImage, Guid userId)
        {
            var premises = await _premiseRepository.ReadById(premisesId, userId);

            if (!string.IsNullOrEmpty(premises.MainImageUrl))
            {
                var imageUrl = await _imageStorage.UpdatePremisesMainImage(userId, newImage, premises.MainImageUrl);
            }
            else
            {
                var imageUrl = await _imageStorage.AddPremisesMainImage(newImage, $"{userId}/premises");
                premises.MainImageUrl = imageUrl;
                await _premiseRepository.Update(premisesId, premises, userId);
            }

            return premises.Id;
        }
    }
}
