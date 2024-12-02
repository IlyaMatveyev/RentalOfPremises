using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IPremiseService
    {
        Task<Guid> Add(Premise premise, Guid userId);
        Task Delete(Guid premisId, Guid userId);
        Task<List<Premise>> GetAll(Guid userId);
        Task<Premise> GetById(Guid premisId, Guid userId);
        Task<Guid> Update(Guid premisId, Premise premise, Guid userId);
    }
}