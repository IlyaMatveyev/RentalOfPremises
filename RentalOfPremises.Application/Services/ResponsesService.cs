using RentalOfPremises.Application.Interfaces;

namespace RentalOfPremises.Application.Services
{
	public class ResponsesService : IResponsesService
	{
		private readonly IResponsesRepository _responsesRepository;

		public ResponsesService(IResponsesRepository responsesRepository)
		{
			_responsesRepository = responsesRepository;
		}

		public async Task<Guid> Create(Guid advertId, string? message)
		{
			return await _responsesRepository.Add(advertId, message);
		}

		public async Task<int> Delete(Guid advertId)
		{
			return await _responsesRepository.Delete(advertId);
		}
	}
}
