using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
	public interface IResponsesRepository
	{
		Task<Guid> Add(Guid advertId, string? message);

		Task<int> Delete (Guid advertId);
	}
}
