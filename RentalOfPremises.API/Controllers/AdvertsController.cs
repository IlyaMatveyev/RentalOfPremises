using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.API.Extensions;
using RentalOfPremises.Application.DTOs.AdvertDto;
using RentalOfPremises.Application.DTOs.Pagination;
using RentalOfPremises.Application.Interfaces;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertsController : ControllerBase
    {
        private readonly IAdvertsService _advertsService;
        private readonly IMapper _mapper;
        public AdvertsController(IAdvertsService advertsService, IMapper mapper)
        {
            _advertsService = advertsService;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet("MyAdverts")]
        public async Task<PaginatedResult<AdvertShortInfoResponse>> GetAll_ForOwner([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            //Достаём userId из клеймов с помощью Extension метода
            var userId = HttpContext.GetUserId();

            var paginationParams = new PaginationParams
            {
                PageSize = pageSize,
                PageNumder = pageNumber
            };

            return await _advertsService.GetAll_ForOwner(paginationParams, userId);
        }

        [HttpGet("Published")]
        public async Task<PaginatedResult<AdvertShortInfoResponse>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var paginationParams = new PaginationParams
            {
                PageSize = pageSize,
                PageNumder = pageNumber
            };

            return await _advertsService.GetAll(paginationParams);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Create([FromBody] AdvertCreateRequest advertCreateRequest)
        {
            //Достаём userId из клеймов с помощью Extension метода
            var userId = HttpContext.GetUserId();

            //mapping
            var advertModel = _mapper.Map<Advert>(advertCreateRequest);
            advertModel.OwnerId = userId;

            return await _advertsService.Create(advertModel);
        }

        [Authorize]
        [HttpGet("MyAdverts/{advertId:guid}")]
        public async Task<ActionResult<AdvertFullInfoResponse>> GetMyAdvertById([FromRoute] Guid advertId)
        {
            return await _advertsService.GetById_ForOwner(advertId);
        }


        [HttpGet("publishedAdverts/{advertId:guid}")]
        public async Task<ActionResult<AdvertFullInfoResponse>> GetById([FromRoute] Guid advertId)
        {
            return await _advertsService.GetById(advertId);
        }

        [Authorize]
        [HttpDelete("{advertId:guid}")]
        public async Task<ActionResult<int>> Delete([FromRoute] Guid advertId)
        {
            return await _advertsService.Delete(advertId);
        }

        [Authorize]
        [HttpPatch("PublishUnpublish/{advertId:guid}")]
        public async Task<ActionResult<Guid>> PublishUnpublish([FromRoute] Guid advertId)
        {
            return await _advertsService.PublishUnpublish(advertId);
        }

        [Authorize]
        [HttpPatch("UpdateInfo/{advertId:guid}")]
        public async Task<ActionResult<Guid>> UpdateInfo([FromBody] AdvertUpdateInfoRequest advertRequest, [FromRoute] Guid advertId)
        {
            return await _advertsService.UpdateInfo(advertRequest, advertId);
        }

        //==============Work with mainImage===================
        [Authorize]
        [HttpPatch("AddMainImage/{advertId:guid}")]
        public async Task<ActionResult<Guid>> UploadMainImage([FromForm] AdvertMainImageRequest request, [FromRoute]Guid advertId)
        {
            return await _advertsService.UploadMainImage(request.Image, advertId);
        }

        [Authorize]
        [HttpPatch("UpdateMainImage/{advertId:guid}")]
        public async Task<ActionResult<Guid>> UpdateMainImage([FromForm] AdvertMainImageRequest request, [FromRoute] Guid advertId)
        {
            return await _advertsService.UpdateMainImage(request.Image, advertId);
        }

        [Authorize]
        [HttpPatch("DeleteMainImage/{advertId:guid}")]
        public async Task<ActionResult<Guid>> DeleteMainImage([FromRoute] Guid advertId)
        {
            return await _advertsService.DeleteMainImage(advertId);
        }

        [Authorize]
        [HttpPost("{advertId:guid}/add-images")]
        public async Task<ActionResult> AddImages([FromRoute]Guid advertId, [FromForm] ImageCollectionRequest images)
        {
            await _advertsService.AddImageCollection(advertId, images);

            //TODO: Надо будет добавить проверку результата (хотябы валидации). Добавить в метод сервиса какое-то возвр. значение
            return Ok("The images will be uploaded soon.");
        }

        [Authorize]
        [HttpDelete("{advertId:guid}/delete-images")]
        public async Task<ActionResult> DeleteImages([FromRoute] Guid advertId, [FromBody] ImageUrlCollectionRequest request)
        {
            await _advertsService.DeleteImageCollection(advertId, request);

            return Ok("The images will be deleted soon.");
        }

        /*
         Методы:
         1) Добавить объявление для Premises (помнить о связи 1:1)  +
         2) Удалить объявление для Premises                         + (но надо будет удалять ещё и фото из облака)
         3) Посмотреть все свои объявления                          +
        3+) Посмотреть все опубликованные объявления                +
         4) Посмотреть своё объявление по ID                        +
         5) Посмотреть чужое опубликованое объявление по ID         +
         6) Изменить информацию в своём объявлении                  +

         Работа с фото отдельно:
         5) Добавить mainImage в объявление                         +
         6) Поменять mainImage в объявлении                         +
         7) Удалить mainImage в объявлении                          +
         
        Работа с коллекцией фото (возможно вынести в отдельный controller)
         8) Добавить коллекцию фото в объявление                    +
         9) Удалить коллекцию фото из объявления                    
         10) Дополнить уже имеющуюся коллекцию коллекцией фото      +
         11) Удалить из коллекции фото коллкцию фото                


         Доп.:
         12) Опубликовать/скрыть объявление из общего доступа       +
         */
    }
}
