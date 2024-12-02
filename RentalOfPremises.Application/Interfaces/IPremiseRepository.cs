using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IPremiseRepository
    {
        Task<Guid> Add(Premise premise);
        Task Delete(Guid id);
        Task<List<Premise>> ReadAll(Guid userId);
        Task<Premise> ReadById(Guid id);
        Task<Guid> Update(Guid id, Premise premise);
    }
}