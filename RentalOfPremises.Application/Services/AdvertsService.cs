using MapsterMapper;
using Microsoft.AspNetCore.Http;
using RentalOfPremises.Application.DTOs.AdvertDto;
using RentalOfPremises.Application.DTOs.Pagination;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Application.Interfaces.Auth;
using RentalOfPremises.Application.Interfaces.Queues;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Services
{
    public class AdvertsService : IAdvertsService
    {
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IAdvertsRepository _advertsRepository;
        private readonly IImageStorage _imageStorage;
        private readonly IImageUploadQueue _imageUploadQueue;
        private readonly IImageDeleteQueue _imageDeleteQueue;
        private readonly IMapper _mapper;
        public AdvertsService(
            IAdvertsRepository advertsRepository,
            IImageStorage imageStorage,
            ICurrentUserContext currentUserContext, 
            IMapper mapper,
            IImageUploadQueue imageUploadQueue,
            IImageDeleteQueue imageDeleteQueue)
        {
            _advertsRepository = advertsRepository;
            _imageStorage = imageStorage;
            _currentUserContext = currentUserContext;
            _mapper = mapper;
            _imageUploadQueue = imageUploadQueue;
            _imageDeleteQueue = imageDeleteQueue;
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

        public async Task<PaginatedResult<AdvertShortInfoResponse>> GetAll_ForOwner(PaginationParams paginationParams, Guid userId)
        {
            var paginatedResultAdvert = await _advertsRepository.ReadAll(paginationParams, userId);

            //mapping
            return new PaginatedResult<AdvertShortInfoResponse>
            {
                Items = _mapper.Map<List<AdvertShortInfoResponse>>(paginatedResultAdvert.Items),
                TotalCount = paginatedResultAdvert.TotalCount,
                PageNumder = paginatedResultAdvert.PageNumder,
                PageSize = paginatedResultAdvert.PageSize
            };
        }

        public async Task<PaginatedResult<AdvertShortInfoResponse>> GetAll(PaginationParams paginationParams)
        {
            var paginatedResultAdvert = await _advertsRepository.ReadAll(paginationParams);

            //mapping
            return new PaginatedResult<AdvertShortInfoResponse>
            {
                Items = _mapper.Map<List<AdvertShortInfoResponse>>(paginatedResultAdvert.Items),
                TotalCount = paginatedResultAdvert.TotalCount,
                PageNumder = paginatedResultAdvert.PageNumder,
                PageSize = paginatedResultAdvert.PageSize
            };
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

        public async Task<Guid> UpdateInfo(AdvertUpdateInfoRequest advertRequest, Guid advertId)
        {
            var advert = _mapper.Map<Advert>(advertRequest);

            return await _advertsRepository.UpdateInfo(advert, advertId);
        }


        //===============Work with MainImage================

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

        //замена главного фото Объявления
        public async Task<Guid> UpdateMainImage(IFormFile newMainImage, Guid advertId)
        {
            if (!_imageStorage.ValidateImageFile(newMainImage))
            {
                throw new Exception("Invalid file!");
            }

            var advert = await _advertsRepository.ReadById(advertId, _currentUserContext.UserId);

            if (!string.IsNullOrEmpty(advert.MainImageUrl))
            {
                await _imageStorage.DeleteImageByUrl(advert.MainImageUrl, _currentUserContext.UserId);
            }

            var imageUrl = await _imageStorage.UploadImage(newMainImage, $"{_currentUserContext.UserId}/Adverts/{advertId}");

            return await _advertsRepository.UpdateMainImage(imageUrl, advertId);
        }

        //удаление главного фото Объявления
        public async Task<Guid> DeleteMainImage(Guid advertId)
        {
            var advert = await _advertsRepository.ReadById(advertId, _currentUserContext.UserId);

            if (string.IsNullOrEmpty(advert.MainImageUrl))
            {
                return advertId;
            }

            var isDeleted = await _imageStorage.DeleteImageByUrl(advert.MainImageUrl, _currentUserContext.UserId);
            if (isDeleted)
            {
                return await _advertsRepository.UpdateMainImage(string.Empty, advertId);
            }
            return Guid.Empty;
        }


        //===============Work with Image Collection================
        public async Task AddImageCollection(Guid advertId, ImageCollectionRequest imageCollectionRequest)
        {
            if(imageCollectionRequest.Images.Count < 1)
            {
                //TODO: можно добавить кастомное исключение
                throw new ArgumentException("Image collection is empty");
            }

            //валидация
            foreach(var image in imageCollectionRequest.Images)
            {
                var isValid = _imageStorage.ValidateImageFile(image);

                if (!isValid)
                {
                    //TODO: можно добавить кастомное исключение
                    throw new ArgumentException("File is invalid");
                }
            }

            //проверка: принадлежит ли этот Advert пользователю
            var advert = await _advertsRepository.ReadById(advertId);
            if (advert.OwnerId == _currentUserContext.UserId)
            {
                //Генерируем таски для бекграунд сервиса
                foreach (var image in imageCollectionRequest.Images)
                {
                    _imageUploadQueue.AddTask(
                        new() { 
                        AdvertId = advertId, 
                        ImageFile = image, 
                        PathInCloud = $"{_currentUserContext.UserId}/Adverts/{advertId}" 
                        });
                }
            }
        }

        public async Task DeleteImageCollection(Guid advertId ,ImageUrlCollectionRequest imageUrlCollectionRequest)
        {
            var advertsOfCurrentUser = await _advertsRepository.ReadAllNonPaginated(_currentUserContext.UserId);

            //проверка принадлежит ли Advert этому пользователю
            if (advertsOfCurrentUser.FirstOrDefault(x => x.Id == advertId) == null)
            {
                throw new KeyNotFoundException("Advert with this Id is not found");
            }
            
            //генерация тасок
            foreach(var url in imageUrlCollectionRequest.Urls)
            {
                _imageDeleteQueue.AddTask(
                    new() { 
                        AdvertId = advertId, 
                        ImageUrl = url, 
                        UserId = _currentUserContext.UserId
                    });
            }
        }


    }
}
