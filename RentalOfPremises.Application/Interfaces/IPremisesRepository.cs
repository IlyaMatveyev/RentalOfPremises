using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IPremisesRepository
    {
        Task<Guid> Add(Premise premise);
        Task<int> Delete(Guid premisesId, Guid userId);
        Task<List<Premise>?> ReadAll(Guid userId);
        Task<Premise> ReadById(Guid premisesId, Guid userId);
        Task<Guid> Update(Guid premisesId, Premise premises, Guid userId);
    }
}