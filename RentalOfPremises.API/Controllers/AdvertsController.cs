using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.API.Extensions;
using RentalOfPremises.Application.DTOs.AdvertDto;
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


        //TODO: для этого метода надо реализовать паггинацию
        /*[HttpGet("MyAdverts")]
        public async Task<ActionResult<List<AdvertShortInfoResponse>>> GetAll()
        {
            //Достаём userId из клеймов с помощью Extension метода
            var userId = HttpContext.GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new
                {
                    error = "Unauthorized",
                    message = "User ID is not valid or missing."
                });
            }

            var listOfAdverts = await _advertsService.GetAll();

        }*/

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

        /*
         Методы:
         1) Добавить объявление для Premises (помнить о связи 1:1)  +
         2) Удалить объявление для Premises                         + (но надо будет удалять ещё и фото из облака)
         3) Посмотреть все свои объявления
         4) Посмотреть своё объявление по ID                        +
         5) Посмотреть чужое опубликованое объявление по ID         + (но надо будет протестить)
         6) Изменить информацию в своём объявлении

         Работа с фото отдельно:
         5) Добавить mainImage в объявление
         6) Поменять mainImage в объявлении
         7) Удалить mainImage в объявлении
         
        Работа с коллекцией фото (возможно вынести в отдельный controller)
         8) Добавить коллекцию фото в объявление
         9) Удалить коллекцию фото из объявления
         10) Дополнить уже имеющуюся коллекцию коллекцией фото
         11) Удалить из коллекции фото набор фото


         Доп.:
         12) Опубликовать/скрыть объявление из общего доступа
         */
    }
}
