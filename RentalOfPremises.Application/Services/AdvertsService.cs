﻿using MapsterMapper;
using Microsoft.AspNetCore.Http;
using RentalOfPremises.Application.DTOs.AdvertDto;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Services
{
    public class AdvertsService : IAdvertsService
    {
        private readonly IAdvertsRepository _advertsRepository;
        private readonly IImageStorage _imageStorage;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IMapper _mapper;
        public AdvertsService(
            IAdvertsRepository advertsRepository,
            IImageStorage imageStorage,
            ICurrentUserContext currentUserContext, 
            IMapper mapper)
        {
            _advertsRepository = advertsRepository;
            _imageStorage = imageStorage;
            _currentUserContext = currentUserContext;
            _mapper = mapper;
        }

        public async Task<Guid> Create(Advert advert)
        {
            return await _advertsRepository.Add(advert);
        }

        public async Task<AdvertFullInfoResponse> GetById_ForOwner(Guid advertId)
        {
            var advertModel = await _advertsRepository.ReadById(advertId, _currentUserContext.UserId);

            //mapping
            return _mapper.Map<AdvertFullInfoResponse>(advertModel);

        }

        public async Task<AdvertFullInfoResponse> GetById(Guid advertId)
        {
            var advertModel = await _advertsRepository.ReadById(advertId);

            //mapping
            return _mapper.Map<AdvertFullInfoResponse>(advertModel);
        }

        public Task<int> Delete(Guid advertId)
        {
            //TODO: Прописать удаление всех фоток из облака
            return _advertsRepository.Delete(advertId);
        }

        public async Task<Guid> PublishUnpublish(Guid advertId)
        {
            return await _advertsRepository.PublishUnpublish(advertId);
        }

        //добавление главного фото Объявления
        public async Task<Guid> UploadMainImage(IFormFile mainImage, Guid advertId)
        {
            if (!_imageStorage.ValidateImageFile(mainImage))
            {
                throw new Exception("Invalid file!");
            }

            var imageUrl = await _imageStorage.UploadImage(mainImage, $"{_currentUserContext.UserId}/Adverts/{advertId}");

            return await _advertsRepository.UpdateMainImage(imageUrl, advertId);
        }
    }
}
