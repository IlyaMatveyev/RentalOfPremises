using Microsoft.AspNetCore.Http;
using RentalOfPremises.Application.DTOs;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Services
{
    public class PremisesService : IPremisesService
    {
        private readonly IPremisesRepository _premiseRepository;
        private readonly IImageStorage _imageStorage;
        private readonly ICurrentUserContext _currentUserContext;
        public PremisesService(
            IPremisesRepository premiseRepository, 
            IImageStorage imageStorage,
            ICurrentUserContext currentUserContext
            )
        {
            _premiseRepository = premiseRepository;
            _imageStorage = imageStorage;
            _currentUserContext = currentUserContext;
        }

        public async Task<Guid> Add(Premises premises, IFormFile? mainImage)
        {
            premises.OwnerId = _currentUserContext.UserId;

            //если есть фото, то добавляем
            if (mainImage != null)
            {
                if (_imageStorage.ValidateImageFile(mainImage))
                {
                    var imageUrl = await _imageStorage.UploadImage(mainImage, $"{_currentUserContext.UserId}/premises");
                    premises.MainImageUrl = imageUrl;
                }
            }


            return await _premiseRepository.Add(premises);
        }

        public async Task<Premises?> GetById(Guid premisesId)
        {
            var premise = await _premiseRepository.ReadById(premisesId);
            
            return premise;
        }

        public async Task<List<Premises>?> GetAll()
        {
            return await _premiseRepository.ReadAll();
        }

        public async Task<int> Delete(Guid premisesId)
        {
            var countOfDelitedObjects = await _premiseRepository.Delete(premisesId);

            return countOfDelitedObjects;
        }

        public async Task<Guid> Update(Guid premisId, Premises premises)
        {
            return await _premiseRepository.Update(premisId, premises);
        }

        public async Task<Guid> UpdateMainImage(Guid premisesId, IFormFile newImage)
        {
            var premises = await _premiseRepository.ReadById(premisesId);

            if (!string.IsNullOrEmpty(premises.MainImageUrl))
            {
                await _imageStorage.DeleteImageByUrl(premises.MainImageUrl, _currentUserContext.UserId);
            }
            var imageUrl = await _imageStorage.UploadImage(newImage, $"{_currentUserContext.UserId}/premises");
            await _premiseRepository.UpdateMainImage(premisesId, imageUrl);

            return premises.Id;
        }
    }
}
