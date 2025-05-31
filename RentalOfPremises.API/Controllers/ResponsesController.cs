using Microsoft.AspNetCore.Mvc;
using RentalOfPremises.Application.DTOs.AdvertResponseDto;
using RentalOfPremises.Application.Interfaces;

namespace RentalOfPremises.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ResponsesController : ControllerBase
	{
		private readonly IResponsesService _responsesService;

		public ResponsesController(IResponsesService responsesService)
		{
			_responsesService = responsesService;
		}

		/// <summary>
		/// Добавление отклика на объявление.
		/// </summary>
		/// <param name="advertResponseCreateRequest">Запрос на создание отклика.</param>
		/// <returns>Id созданного отклика.</returns>
		[HttpPost("Create")]
		public async Task<ActionResult<Guid>> Create([FromBody] AdvertResponseCreateRequest advertResponseCreateRequest)
		{
			return await _responsesService.Create(
				advertResponseCreateRequest.PublishedAdvertId, 
				advertResponseCreateRequest.Message
				);
		}

		/// <summary>
		/// Удаляет отклик.
		/// </summary>
		/// <param name="publishedAdvertId">Id опубликованного объявления.</param>
		/// <returns>Количество удалённых записей из БД.</returns>
		[HttpDelete("Delete/{publishedAdvertId:guid}")]
		public async Task<ActionResult<int>> Delete([FromRoute] Guid publishedAdvertId)
		{
			return await _responsesService.Delete(publishedAdvertId);
		}
	}
}
